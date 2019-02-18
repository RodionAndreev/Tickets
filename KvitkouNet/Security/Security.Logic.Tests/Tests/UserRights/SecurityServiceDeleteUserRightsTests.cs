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

namespace Security.Logic.Tests.Tests.UserRights
{
    public class SecurityServiceDeleteUserRightsTests
    {
        private IUserRightsService _securityData;
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
            _mock.Setup(x => x.DeleteUserRights(It.Is<string>(id =>
                    _dbFaker.UserRights.Any(l => l.UserId == id))))
                .Returns<string>(id =>
                {
                    return Task.FromResult(true);
                });

            //not exists
            _mock.Setup(x => x.DeleteUserRights(It.Is<string>(id =>
                    _dbFaker.UserRights.All(l => l.UserId != id))))
                .Returns<string>(id =>
                {
                    throw new SecurityDbException("not exists", ExceptionType.NotFound,
                        EntityType.Role, new[] { id.ToString() });
                });

            _securityData = new UserRightsService(_mock.Object, _mapper, new UserRightsValidator(), new AccessRequestValidator());
        }

        [Test]
        public async Task DeleteUserRightsEmpty()
        {
            var result = await _securityData.DeleteUserRights("");
            var expectedMessage = "Nothing was deleted on empty id";

            Assert.AreEqual(ActionStatus.Warning, result.Status);
            Assert.AreEqual(expectedMessage, result.Message);
            _mock.Verify(data => data.DeleteUserRights(""), () => Times.Exactly(0));
        }

        [Test]
        public async Task DeleteUserRightsNotExisted()
        {
            var id = "NotExisted";
            var result = await _securityData.DeleteUserRights(id);
            var expectedMessage = $"Role with id = {id} was not found";

            Assert.AreEqual(ActionStatus.Warning, result.Status);
            Assert.AreEqual(expectedMessage, result.Message);
            _mock.Verify(data => data.DeleteUserRights(id), () => Times.Exactly(1));
        }

        [Test]
        public async Task DeleteUserRights()
        {
            var id = _dbFaker.UserRights.FirstOrDefault().UserId;
            var result = await _securityData.DeleteUserRights(id);

            Assert.AreEqual(ActionStatus.Success, result.Status);
            _mock.Verify(data => data.DeleteUserRights(id), () => Times.Exactly(1));
        }
    }
}