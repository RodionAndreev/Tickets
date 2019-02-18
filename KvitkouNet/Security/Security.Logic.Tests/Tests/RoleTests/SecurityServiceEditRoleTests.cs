using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Security.Data;
using Security.Data.Exceptions;
using Security.Data.Models;
using Security.Logic.Implementations;
using Security.Logic.MappingProfiles;
using Security.Logic.Models;
using Security.Logic.Models.Enums;
using Security.Logic.Services;
using Security.Logic.Tests.Fakers;
using Security.Logic.Validators;

namespace Security.Logic.Tests.Tests.RoleTests
{
    public class SecurityServiceEditRoleTests
    {
        private IRoleService _securityData;
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
            _mock.Setup(x => x.EditRoleRights(It.Is<int>(id =>
                    _dbFaker.Roles.Any(k => k.Id.Equals(id))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l)))))
                .Returns<int, int[], int[]>((id, aIds, dIds) =>
                {
                    return Task.FromResult(true);
                });

            //not existed role
            _mock.Setup(x => x.EditRoleRights(It.Is<int>(id =>
                    !_dbFaker.Roles.Any(k => k.Id.Equals(id))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l)))))
                .Returns<int, int[], int[]>((id, aIds, dIds) =>
                {
                    throw new SecurityDbException("not existed role", ExceptionType.NotFound,
                        EntityType.Role, new[] { id.ToString() });
                });

            //not existed rights 1
            _mock.Setup(x => x.EditRoleRights(It.Is<int>(id =>
                    _dbFaker.Roles.Any(k => k.Id.Equals(id))), It.Is<int[]>(ids =>
                    !ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l)))))
                .Returns<int, int[], int[]>((id, aIds, dIds) =>
                {
                    throw new SecurityDbException("not existed rights", ExceptionType.NotFound,
                        EntityType.Right, aIds.Select(l => l.ToString()).ToArray());
                });

            //not existed rights 2
            _mock.Setup(x => x.EditRoleRights(It.Is<int>(id =>
                    _dbFaker.Roles.Any(k => k.Id.Equals(id))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l))), It.Is<int[]>(ids =>
                    !ids.All(l => _dbFaker.AccessRights.Any(k => k.Id == l)))))
                .Returns<int, int[], int[]>((id, aIds, dIds) =>
                {
                    throw new SecurityDbException("not existed rights", ExceptionType.NotFound,
                        EntityType.Right, dIds.Select(l => l.ToString()).ToArray());
                });

            //success
            _mock.Setup(x => x.EditRoleFunctions(It.Is<int>(id =>
                    _dbFaker.Roles.Any(k => k.Id.Equals(id))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.Functions.Any(k => k.Id == l)))))
                .Returns<int, int[]>((id,iIds) =>
                {
                    return Task.FromResult(true);
                });

            //not existed role
            _mock.Setup(x => x.EditRoleFunctions(It.Is<int>(id =>
                    !_dbFaker.Roles.Any(k => k.Id.Equals(id))), It.Is<int[]>(ids =>
                    ids.All(l => _dbFaker.Functions.Any(k => k.Id == l)))))
                .Returns<int, int[]>((id, ids) =>
                {
                    throw new SecurityDbException("not existed role", ExceptionType.NotFound,
                        EntityType.Role, new[] { id.ToString() });
                });

            //not existed function 
            _mock.Setup(x => x.EditRoleFunctions(It.Is<int>(id =>
                    _dbFaker.Roles.Any(k => k.Id.Equals(id))), It.Is<int[]>(ids =>
                    !ids.All(l => _dbFaker.Functions.Any(k => k.Id == l)))))
                .Returns<int, int[]>((id, ids) =>
                {
                    throw new SecurityDbException("not existed function", ExceptionType.NotFound,
                        EntityType.Function, ids.Select(l => l.ToString()).ToArray());
                });

            _securityData = new RoleService(_mock.Object, _mapper);
        }

        [Test]
        public async Task EditRoleRights()
        {
            var id = _dbFaker.Roles.FirstOrDefault().Id;
            var aIds = _dbFaker.AccessRights.Take(3).Select(l => l.Id).ToArray();
            var dIds = _dbFaker.AccessRights.Skip(3).Take(3).Select(l => l.Id).ToArray();
            var fIds = _dbFaker.Functions.Skip(3).Take(3).Select(l => l.Id).ToArray();

            var response = await _securityData.EditRole(id, aIds, dIds, fIds);

            Assert.AreEqual(ActionStatus.Success, response.Status, response.Message);
            _mock.Verify(data => data.EditRoleRights(id, aIds, dIds), () => Times.Exactly(1));
            _mock.Verify(data => data.EditRoleFunctions(id, fIds), () => Times.Exactly(1));
        }

        [Test]
        public async Task EditRoleRightsNotExistedRole()
        {
            var id = _dbFaker.Roles.Max(l=>l.Id) + 1;
            var aIds = _dbFaker.AccessRights.Take(3).Select(l => l.Id).ToArray();
            var dIds = _dbFaker.AccessRights.Skip(3).Take(3).Select(l => l.Id).ToArray();
            var fIds = _dbFaker.Functions.Skip(3).Take(3).Select(l => l.Id).ToArray();

            var response = await _securityData.EditRole(id, aIds, dIds, fIds);
            var expectedMessage = $"Role with id = {id} was not found";

            Assert.AreEqual(ActionStatus.Warning, response.Status, response.Message);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.EditRoleRights(id, aIds, dIds), () => Times.Exactly(0));
            _mock.Verify(data => data.EditRoleFunctions(id, fIds), () => Times.Exactly(1));
        }

        [Test]
        public async Task EditRoleRightsNotExistedFunction()
        {
            var id = _dbFaker.Roles.FirstOrDefault().Id;
            var aIds = _dbFaker.AccessRights.Take(3).Select(l => l.Id).ToArray();
            var dIds = _dbFaker.AccessRights.Skip(3).Take(3).Select(l => l.Id).ToArray();
            var fId1 = _dbFaker.Functions.Max(l=>l.Id) + 1;
            var fId2 = _dbFaker.Functions.Max(l=>l.Id) + 2;
            var fIds = new []{fId1, fId2};

            var response = await _securityData.EditRole(id, aIds, dIds, fIds);
            var expectedMessage = $"Access Function with id = {string.Join(",", fIds.Select(l=>l.ToString()))} was not found";

            Assert.AreEqual(ActionStatus.Warning, response.Status, response.Message);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.EditRoleRights(id, aIds, dIds), () => Times.Exactly(0));
            _mock.Verify(data => data.EditRoleFunctions(id, fIds), () => Times.Exactly(1));
        }

        [Test]
        public async Task EditRoleRightsNotExistedRights1()
        {
            var id = _dbFaker.Roles.FirstOrDefault().Id;
            var aId1 = _dbFaker.AccessRights.Max(l=>l.Id) + 1;
            var aId2 = _dbFaker.AccessRights.Max(l=>l.Id) + 2;
            var aIds = new []{aId1, aId2};
            var dIds = _dbFaker.AccessRights.Skip(3).Take(3).Select(l => l.Id).ToArray();
            var fIds = _dbFaker.Functions.Skip(3).Take(3).Select(l => l.Id).ToArray();

            var response = await _securityData.EditRole(id, aIds, dIds, fIds);
            var expectedMessage = $"Access Right with id = {string.Join(",", aIds.Select(l => l.ToString()))} was not found";

            Assert.AreEqual(ActionStatus.Warning, response.Status, response.Message);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.EditRoleRights(id, aIds, dIds), () => Times.Exactly(1));
            _mock.Verify(data => data.EditRoleFunctions(id, fIds), () => Times.Exactly(1));
        }

        [Test]
        public async Task EditRoleRightsNotExistedRights2()
        {
            var id = _dbFaker.Roles.FirstOrDefault().Id;
            var aIds = _dbFaker.AccessRights.Skip(3).Take(3).Select(l => l.Id).ToArray();
            var dId1 = _dbFaker.AccessRights.Max(l => l.Id) + 1;
            var dId2 = _dbFaker.AccessRights.Max(l => l.Id) + 2;
            var dIds = new[] { dId1, dId2 };
            var fIds = _dbFaker.Functions.Skip(3).Take(3).Select(l => l.Id).ToArray();

            var response = await _securityData.EditRole(id, aIds, dIds, fIds);
            var expectedMessage = $"Access Right with id = {string.Join(",", dIds.Select(l => l.ToString()))} was not found";

            Assert.AreEqual(ActionStatus.Warning, response.Status, response.Message);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.EditRoleRights(id, aIds, dIds), () => Times.Exactly(1));
            _mock.Verify(data => data.EditRoleFunctions(id, fIds), () => Times.Exactly(1));
        }

        [Test]
        public async Task EditRoleRightsNotCrossedRights()
        {
            var id = _dbFaker.Roles.FirstOrDefault().Id;
            var aIds = _dbFaker.AccessRights.Skip(3).Take(3).Select(l => l.Id).ToArray();
            var dIds = _dbFaker.AccessRights.Skip(2).Take(3).Select(l => l.Id).ToArray();
            var fIds = _dbFaker.Functions.Skip(3).Take(3).Select(l => l.Id).ToArray();

            var response = await _securityData.EditRole(id, aIds, dIds, fIds);
            var expectedMessage = "Accessed and denied must not have same Rights";

            Assert.AreEqual(ActionStatus.Warning, response.Status, response.Message);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.EditRoleRights(id, aIds, dIds), () => Times.Exactly(0));
            _mock.Verify(data => data.EditRoleFunctions(id, fIds), () => Times.Exactly(0));
        }
    }
}