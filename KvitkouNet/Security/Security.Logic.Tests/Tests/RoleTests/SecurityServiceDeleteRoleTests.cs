using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Security.Data;
using Security.Data.Exceptions;
using Security.Logic.Implementations;
using Security.Logic.MappingProfiles;
using Security.Logic.Models.Enums;
using Security.Logic.Services;
using Security.Logic.Tests.Fakers;
using Security.Logic.Validators;

namespace Security.Logic.Tests.Tests.RoleTests
{
    public class SecurityServiceDeleteRoleTests
    {
        private IRoleService _securityData;
        private IMapper _mapper;
        private Mock<ISecurityData> _mock;
        private SecurityDbFaker _dbFaker;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccessRightProfile>();
                cfg.AddProfile<RoleProfile>();
            }));

            _dbFaker = new SecurityDbFaker();

            _mock = new Mock<ISecurityData>();

            //success
            _mock.Setup(x => x.DeleteRole(It.Is<int>(id =>
                    _dbFaker.Roles.Any(l => l.Id == id))))
                .Returns<int>(id =>
                {
                    return Task.FromResult(true);
                });

            //not exists
            _mock.Setup(x => x.DeleteRole(It.Is<int>(id =>
                    _dbFaker.Roles.All(l => l.Id != id))))
                .Returns<int>(id =>
                {
                    throw new SecurityDbException("not exists", ExceptionType.NotFound,
                        EntityType.Role, new[] { id.ToString() });
                });

            _securityData = new RoleService(_mock.Object, _mapper);
        }

        [Test]
        public async Task DeleteRoleNotExisted()
        {
            var id = _dbFaker.Roles.Max(l => l.Id) + 1;

            var result = await _securityData.DeleteRole(id);
            var expectedMessage = $"Role with id = {id} was not found";

            Assert.AreEqual(ActionStatus.Warning, result.Status);
            Assert.AreEqual(expectedMessage, result.Message);
            _mock.Verify(data => data.DeleteRole(It.Is<int>(db => db == 0 )), () => Times.Exactly(0));
        }

        [Test]
        public async Task DeleteRole()
        {
            var id = _dbFaker.Roles.Max(l => l.Id);

            var result = await _securityData.DeleteRole(id);

            Assert.AreEqual(ActionStatus.Success, result.Status);
            _mock.Verify(data => data.DeleteRole(It.Is<int>(db => db == id )), () => Times.Exactly(1));
        }
    }
}