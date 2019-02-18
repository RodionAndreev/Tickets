using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Internal;
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

namespace Security.Logic.Tests.Tests.AccessRightTests
{
    public class SecurityServiceDeleteFunctionsTests
    {
        private IRightsService _securityData;
        private SecurityDbFaker _dbFaker;
        private IMapper _mapper;
        private Mock<ISecurityData> _mock;

        [SetUp]
        public void Setup()
        {
            _dbFaker = new SecurityDbFaker();
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccessRightProfile>();
            }));

            _mock = new Mock<ISecurityData>();

            //success
            _mock.Setup(x => x.DeleteRight(It.Is<int>(right =>
                    _dbFaker.AccessRights.Any(l=>l.Id == right))))
                .Returns<int>(id =>
                {
                    return Task.FromResult(true);
                });

            //not exists
            _mock.Setup(x => x.DeleteRight(It.Is<int>(right =>
                    _dbFaker.AccessRights.All(l => l.Id != right))))
                .Returns<int>(id =>
                {
                    throw new SecurityDbException("not exists", ExceptionType.NotFound,
                        EntityType.Right, new[] {id.ToString()});
                });

            _securityData = new RightsService(_mock.Object, _mapper);
        }

        [Test]
        public async Task DeleteRightNotExisted()
        {
            var id = _dbFaker.AccessRights.Max(l=>l.Id) + 1;

            var result = await _securityData.DeleteRight(id);
            var expectedMessage = $"Access Right with id = {id} was not found";

            Assert.AreEqual(ActionStatus.Warning, result.Status);
            Assert.AreEqual(expectedMessage, result.Message);
            _mock.Verify(data => data.DeleteRight(It.Is<int>(db => db == 0 )), () => Times.Exactly(0));
        }

        [Test]
        public async Task DeleteRight()
        {
            var id = _dbFaker.AccessRights.Max(l => l.Id);

            var result = await _securityData.DeleteRight(id);

            Assert.AreEqual(ActionStatus.Success, result.Status);
            _mock.Verify(data => data.DeleteRight(It.Is<int>(db => db == id )), () => Times.Exactly(1));
        }
    }
}