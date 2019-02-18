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
using Security.Logic.Tests.Comparers;
using Security.Logic.Tests.Fakers;
using Security.Logic.Validators;

namespace Security.Logic.Tests.Tests.UserRights
{
    public class SecurityServiceGetUserRightsTests
    {
        private IUserRightsService _securityData;
        private SecurityDbFaker _dbFaker;
        private IMapper _mapper;
        private Mock<ISecurityData> _mock;

        [SetUp]
        public void Setup()
        {
            _dbFaker = new SecurityDbFaker();
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserRightsProfile>();
            }));

            _mock = new Mock<ISecurityData>();
            _mock.Setup(x => x.GetUserRights(It.IsAny<string>()))
                .Returns((string userId) =>
                    Task.FromResult(
                        _dbFaker.UserRights
                            .Single(l => l.UserId == userId)));

            _mock.Setup(x => x.GetUserRights(It.Is<string>(s => 
                    _dbFaker.UserRights.All(l => l.UserId != s))))
                .Returns((string userId) => 
                    throw new SecurityDbException("User rights was not found", 
                        ExceptionType.NotFound, 
                        EntityType.UserRights, 
                        new[] { userId }));

            _securityData = new UserRightsService(_mock.Object, _mapper, 
                new UserRightsValidator(), new AccessRequestValidator());
        }
        
        [Test]
        public async Task GetUserRights()
        {
            var userId = _dbFaker.UserRights.First().UserId;

            var response = await _securityData.GetUserRights(userId);
            var expected = _mapper.Map<Models.UserRights>(_dbFaker.UserRights.SingleOrDefault(l=>l.UserId == userId));

            Assert.AreEqual(ActionStatus.Success, response.Status);
            CollectionAssert.AreEqual(expected.AccessRights, response.UserRights.AccessRights, new AccessRightComparer());
            CollectionAssert.AreEqual(expected.DeniedRights, response.UserRights.DeniedRights, new AccessRightComparer());
            CollectionAssert.AreEqual(expected.AccessFunctions, response.UserRights.AccessFunctions, new FunctionComparer());
            CollectionAssert.AreEqual(expected.Roles, response.UserRights.Roles, new RoleComparer());
            _mock.Verify(data => data.GetUserRights(It.Is<string>(i => i == userId)), () => Times.Exactly(1));
        }

        [Test]
        public async Task GetUserRightsToLongMask()
        {
            var userId = "no423534645674tgdfbdfmgvbngdnty356y34gvt634fredgdfhnfgmytngv56t43c5t23rhfghnfgjgdfbdfmgvbngdnty356y34gvt634fredgdfhnfgmytngv56t43c5t23rhfghnfgj";
            
            var response = await _securityData.GetUserRights(userId);
            var expectedMessage = "UserId longer then 100";

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.GetUserRights(It.Is<string>(i => i == userId)), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetUserRightsMaskStartsEndsWithSpaces()
        {
            var userId = "notExists";

            var response = await _securityData.GetUserRights(userId);
            var expectedMessage = $"User Rights with id = {userId} was not found";

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.GetUserRights(It.Is<string>(i => i == userId)), () => Times.Exactly(1));
        }
    }
}