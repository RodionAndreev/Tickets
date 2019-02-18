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
    public class SecurityServiceAddRoleTests
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
                cfg.AddProfile<RoleProfile>();
            }));

            _mock = new Mock<ISecurityData>();

            //success
            _mock.Setup(x => x.AddRole(It.Is<string>(name => 
                    !_dbFaker.Roles.Any(l=>l.Name.Equals(name)))))
                .Returns(() =>
                {
                    return Task.FromResult(_dbFaker.Roles.Max(l => l.Id) + 1);
                });

            //name existed
            _mock.Setup(x => x.AddRole(It.Is<string>(name => 
                    _dbFaker.Roles.Any(l=>l.Name.Equals(name)))))
                .Returns<string>(name =>
                {
                    throw new SecurityDbException("name existed", ExceptionType.NameExists,
                        EntityType.Role, new []{name});
                });

            //error
            _mock.Setup(x => x.AddRole(It.Is<string>(name => 
                   name.Equals("Error!"))))
                .Returns<string>(name =>
                {
                    throw new Exception();
                });
            
            _securityData = new RoleService(_mock.Object, _mapper);
        }

        [Test]
        public async Task AddRole()
        {
            var roleName = "NormalName";

            var roles = await _securityData.AddRole(roleName);
            var expected = _dbFaker.Roles.Max(l => l.Id) + 1;

            Assert.AreEqual(ActionStatus.Success, roles.Status);
            Assert.AreEqual(expected, roles.Id);
            _mock.Verify(data => data.AddRole(roleName), () => Times.Exactly(1));
        }

        [Test]
        public async Task AddRoleExistedName()
        {
            var existedName = _dbFaker.Roles.FirstOrDefault()?.Name;

            var roles = await _securityData.AddRole(existedName);
            var expectedMessage = $"Names: {existedName} of Role already exist";

            Assert.AreEqual(ActionStatus.Warning, roles.Status);
            Assert.AreEqual(expectedMessage, roles.Message);
            _mock.Verify(data => data.AddRole(existedName), () => Times.Exactly(1));
        }

        [Test]
        public async Task AddRoleToLongName()
        {
            var roleName = "In the fields of physical security and information security, access control(AC) is the selective restriction of access to a place or other resource";

            var roles = await _securityData.AddRole(roleName);
            var expectedMessage = "Name must be between 1 and 100 characters";

            Assert.AreEqual(ActionStatus.Warning, roles.Status);
            Assert.AreEqual(expectedMessage, roles.Message);
            _mock.Verify(data => data.AddRole(roleName), () => Times.Exactly(0));
        }

        [Test]
        public async Task AddRoleUnknownError()
        {
            var roleName = "Error!";

            var roles = await _securityData.AddRole(roleName);
            var expectedMessage = "Something went wrong!";

            Assert.AreEqual(ActionStatus.Error, roles.Status);
            Assert.AreEqual(expectedMessage, roles.Message);
            _mock.Verify(data => data.AddRole(roleName), () => Times.Exactly(1));
        }
    }
}