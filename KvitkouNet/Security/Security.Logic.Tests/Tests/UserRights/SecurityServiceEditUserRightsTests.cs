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
    public class SecurityServiceEditUserRightsTests
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
                cfg.AddProfile<AccessRightProfile>();
                cfg.AddProfile<FeatureProfile>();
            }));

            _mock = new Mock<ISecurityData>();

            //success
            _mock.Setup(x => x.EditUserRights(It.Is<string>(id =>
                    _dbFaker.UserRights.Any(k => k.UserId.Equals(id))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.Roles.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.Functions.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l)))))
                .Returns<string, int[], int[], int[], int[]>((id, rIds, fIds, aIds, dIds) =>
                {
                    return Task.FromResult(true);
                });

            //not existed userRights
            _mock.Setup(x => x.EditUserRights(It.Is<string>(id =>
                    !_dbFaker.UserRights.Any(k => k.UserId.Equals(id))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.Roles.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.Functions.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l)))))
                .Returns<string, int[], int[], int[], int[]>((id, rIds, fIds, aIds, dIds) =>
                {
                    throw new SecurityDbException("", ExceptionType.NotFound,
                        EntityType.UserRights, new []{id});
                });

            //not existed role
            _mock.Setup(x => x.EditUserRights(It.Is<string>(id =>
                    _dbFaker.UserRights.Any(k => k.UserId.Equals(id))), It.Is<int[]>(ids =>
                    !ids.All(l => _dbFaker.Roles.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.Functions.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l)))))
                .Returns<string, int[], int[], int[], int[]>((id, rIds, fIds, aIds, dIds) =>
                {
                    throw new SecurityDbException("", ExceptionType.NotFound,
                        EntityType.Role, rIds.Select(l => l.ToString()).ToArray());
                });

            //not existed rights 1
            _mock.Setup(x => x.EditUserRights(It.Is<string>(id =>
                    _dbFaker.UserRights.Any(k => k.UserId.Equals(id))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.Roles.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.Functions.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    !ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l)))))
                .Returns<string, int[], int[], int[], int[]>((id, rIds, fIds, aIds, dIds) =>
                {
                    throw new SecurityDbException("", ExceptionType.NotFound,
                        EntityType.Right, aIds.Select(l => l.ToString()).ToArray());
                });

            //not existed rights 2
            _mock.Setup(x => x.EditUserRights(It.Is<string>(id =>
                    _dbFaker.UserRights.Any(k => k.UserId.Equals(id))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.Roles.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.Functions.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    !ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l)))))
                .Returns<string, int[], int[], int[], int[]>((id, rIds, fIds, aIds, dIds) =>
                {
                    throw new SecurityDbException("", ExceptionType.NotFound,
                        EntityType.Right, dIds.Select(l => l.ToString()).ToArray());
                });

            //not existed function 
            _mock.Setup(x => x.EditUserRights(It.Is<string>(id =>
                    _dbFaker.UserRights.Any(k => k.UserId.Equals(id))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.Roles.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    !ids.All(l => _dbFaker.Functions.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l)))))
                .Returns<string, int[], int[], int[], int[]>((id, rIds, fIds, aIds, dIds) =>
                {
                    throw new SecurityDbException("", ExceptionType.NotFound,
                        EntityType.Function, fIds.Select(l => l.ToString()).ToArray());
                });

            _securityData = new UserRightsService(_mock.Object, _mapper,
                new UserRightsValidator(), new AccessRequestValidator());
        }

        [Test]
        public async Task EditUserRightsRights()
        {
            var id = _dbFaker.UserRights.FirstOrDefault().UserId;
            var rIds = _dbFaker.Roles.Take(3).Select(l => l.Id).ToArray();
            var fIds = _dbFaker.Functions.Skip(3).Take(3).Select(l => l.Id).ToArray();
            var aIds = _dbFaker.AccessRights.Take(3).Select(l => l.Id).ToArray();
            var dIds = _dbFaker.AccessRights.Skip(3).Take(3).Select(l => l.Id).ToArray();

            var response = await _securityData.EditUserRights(id, rIds, fIds, aIds, dIds);

            Assert.AreEqual(ActionStatus.Success, response.Status, response.Message);
            _mock.Verify(data => data.EditUserRights(id, rIds, fIds, aIds, dIds), () => Times.Exactly(1));
        }

        [Test]
        public async Task EditUserRightsRightsNotExistedUserRights()
        {
            var id = "NotExisted";
            var rIds = _dbFaker.Roles.Take(3).Select(l => l.Id).ToArray();
            var fIds = _dbFaker.Functions.Skip(3).Take(3).Select(l => l.Id).ToArray();
            var aIds = _dbFaker.AccessRights.Take(3).Select(l => l.Id).ToArray();
            var dIds = _dbFaker.AccessRights.Skip(3).Take(3).Select(l => l.Id).ToArray();
            
            var response = await _securityData.EditUserRights(id, rIds, fIds, aIds, dIds);
            var expectedMessage = $"User Rights with id = {id} was not found";

            Assert.AreEqual(ActionStatus.Warning, response.Status, response.Message);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.EditUserRights(id, rIds, fIds, aIds, dIds), () => Times.Exactly(1));
        }

        [Test]
        public async Task EditUserRightsRightsNotExistedRole()
        {
            var id = _dbFaker.UserRights.FirstOrDefault().UserId;
            var rId1 = _dbFaker.Roles.Max(l => l.Id) + 1;
            var rId2 = _dbFaker.Roles.Max(l => l.Id) + 2;
            var rIds = new[] { rId1, rId2 };
            var fIds = _dbFaker.Functions.Skip(3).Take(3).Select(l => l.Id).ToArray();
            var aIds = _dbFaker.AccessRights.Take(3).Select(l => l.Id).ToArray();
            var dIds = _dbFaker.AccessRights.Skip(3).Take(3).Select(l => l.Id).ToArray();
            
            var response = await _securityData.EditUserRights(id, rIds, fIds, aIds, dIds);
            var expectedMessage = $"Role with id = {string.Join(",", rIds.Select(l => l.ToString()))} was not found";

            Assert.AreEqual(ActionStatus.Warning, response.Status, response.Message);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.EditUserRights(id, rIds, fIds, aIds, dIds), () => Times.Exactly(1));
        }

        [Test]
        public async Task EditUserRightsRightsNotExistedFunction()
        {
            var id = "NotExisted";
            var rIds = _dbFaker.Roles.Take(3).Select(l => l.Id).ToArray();
            var fIds = _dbFaker.Functions.Skip(3).Take(3).Select(l => l.Id).ToArray();
            var aIds = _dbFaker.AccessRights.Take(3).Select(l => l.Id).ToArray();
            var dIds = _dbFaker.AccessRights.Skip(3).Take(3).Select(l => l.Id).ToArray();
            

            var response = await _securityData.EditUserRights(id, rIds, fIds, aIds, dIds);
            var expectedMessage = $"User Rights with id = {id} was not found";

            Assert.AreEqual(ActionStatus.Warning, response.Status, response.Message);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.EditUserRights(id, rIds, fIds, aIds, dIds), () => Times.Exactly(1));
        }

        [Test]
        public async Task EditUserRightsRightsNotExistedRights1()
        {
            var id = _dbFaker.UserRights.FirstOrDefault().UserId;
            var rIds = _dbFaker.Roles.Take(3).Select(l => l.Id).ToArray();
            var fIds = _dbFaker.Functions.Skip(3).Take(3).Select(l => l.Id).ToArray();
            var aId1 = _dbFaker.AccessRights.Max(l => l.Id) + 1;
            var aId2 = _dbFaker.AccessRights.Max(l => l.Id) + 2;
            var aIds = new[] { aId1, aId2 };
            var dIds = _dbFaker.AccessRights.Skip(3).Take(3).Select(l => l.Id).ToArray();
            
            var response = await _securityData.EditUserRights(id, rIds, fIds, aIds, dIds);
            var expectedMessage = $"Access Right with id = {string.Join(",", aIds.Select(l => l.ToString()))} was not found";

            Assert.AreEqual(ActionStatus.Warning, response.Status, response.Message);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.EditUserRights(id, rIds, fIds, aIds, dIds), () => Times.Exactly(1));
        }

        [Test]
        public async Task EditUserRightsRightsNotExistedRights2()
        {
            var id = _dbFaker.UserRights.FirstOrDefault().UserId;
            var rIds = _dbFaker.Roles.Take(3).Select(l => l.Id).ToArray();
            var fIds = _dbFaker.Functions.Skip(3).Take(3).Select(l => l.Id).ToArray();
            var aIds = _dbFaker.AccessRights.Take(3).Select(l => l.Id).ToArray();
            var dId1 = _dbFaker.AccessRights.Max(l => l.Id) + 1;
            var dId2 = _dbFaker.AccessRights.Max(l => l.Id) + 2;
            var dIds = new[] { dId1, dId2 };

            var response = await _securityData.EditUserRights(id, rIds, fIds, aIds, dIds);
            var expectedMessage = $"Access Right with id = {string.Join(",", dIds.Select(l => l.ToString()))} was not found";

            Assert.AreEqual(ActionStatus.Warning, response.Status, response.Message);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.EditUserRights(id, rIds, fIds, aIds, dIds), () => Times.Exactly(1));
        }

        [Test]
        public async Task EditUserRightsRightsNotCrossedRights()
        {
            var id = _dbFaker.UserRights.FirstOrDefault().UserId;
            var rIds = _dbFaker.Roles.Take(3).Select(l => l.Id).ToArray();
            var fIds = _dbFaker.Functions.Skip(3).Take(3).Select(l => l.Id).ToArray();
            var aIds = _dbFaker.AccessRights.Take(3).Select(l => l.Id).ToArray();
            var dIds = _dbFaker.AccessRights.Skip(2).Take(3).Select(l => l.Id).ToArray();

            var response = await _securityData.EditUserRights(id, rIds, fIds, aIds, dIds);
            var expectedMessage = "Accessed and denied must not have same Rights";

            Assert.AreEqual(ActionStatus.Warning, response.Status, response.Message);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.EditUserRights(id, rIds, fIds, aIds, dIds), () => Times.Exactly(0));
        }
    }
}