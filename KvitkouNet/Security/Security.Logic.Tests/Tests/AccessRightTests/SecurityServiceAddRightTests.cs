using System;
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
using Security.Logic.Models.Enums;
using Security.Logic.Services;
using Security.Logic.Tests.Fakers;

namespace Security.Logic.Tests.Tests.AccessRightTests
{
    public class SecurityServiceGetFunctionsTests
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
                cfg.AddProfile<AccessFunctionProfile>();
                cfg.AddProfile<FeatureProfile>();
                cfg.AddProfile<RoleProfile>();
                cfg.AddProfile<UserRightsProfile>();
            }));

            _mock = new Mock<ISecurityData>();
            //success
            _mock.Setup(x => x.AddRights(It.Is<string[]>(right =>
                    !right.Any(l => _dbFaker.AccessRights.Any(k => k.Name == l)))))
                .Returns<string[]>(names =>
                {
                    return Task.FromResult(names.Select((name, i) => 
                        new AccessRightDb
                        {
                            Name = name,
                            Id = _dbFaker.AccessRights.Max(l => l.Id) + i + 1
                        }).ToArray());
                });
            
            //existed name
            _mock.Setup(x => x.AddRights(It.Is<string[]>(right => 
                    right.Any(l=>_dbFaker.AccessRights.Any(k=>k.Name == l)))))
                .Returns<string[]>(names =>
                {
                    throw new SecurityDbException("existed name", ExceptionType.NameExists,
                        EntityType.Right, names);
                });
            
            //unknown error
            _mock.Setup(x => x.AddRights(It.Is<string[]>(right => 
                    right.Any(l=> l.Equals("Error!")))))
                .Returns<string[]>(names =>
                {
                    throw new Exception();
                });

            _securityData = new RightsService(_mock.Object, _mapper);
        }

        [Test]
        public async Task AddRight()
        {
            var rightNames = new []{"name1"};

            var rights = await _securityData.AddRights(rightNames);
            var expectedId = _dbFaker.AccessRights.Max(l => l.Id) + 1;

            Assert.AreEqual(ActionStatus.Success, rights.Status);
            Assert.AreEqual(expectedId, rights.AccessRights.SingleOrDefault().Id);
            _mock.Verify(data => data.AddRights(rightNames), () => Times.Exactly(1));
        }

        [Test]
        public async Task AddRightExistedName()
        {
            var existedName = _dbFaker.AccessRights.FirstOrDefault()?.Name;
            var rightNames = new[] { existedName };

            var rights = await _securityData.AddRights(rightNames);
            var expectedMessage = $"Names: {existedName} of Access Right already exist";

            Assert.AreEqual(ActionStatus.Warning, rights.Status);
            Assert.AreEqual(expectedMessage, rights.Message);
            _mock.Verify(data => data.AddRights(rightNames), () => Times.Exactly(1));
        }

        [Test]
        public async Task AddRightToLongName()
        {
            var longName = "In the fields of physical security and information security, access control(AC) is the selective restriction of access to a place or other resource";
            var rightNames = new[] { longName };

            var rights = await _securityData.AddRights(rightNames);
            var expectedMessage = "Name must be between 1 and 100 characters";

            Assert.AreEqual(ActionStatus.Warning, rights.Status);
            Assert.AreEqual(expectedMessage, rights.Message);
            _mock.Verify(data => data.AddRights(rightNames), () => Times.Exactly(0));
        }

        [Test]
        public async Task AddRightUnknownError()
        {
            var errorName = "Error!";
            var rightNames = new[] { errorName };

            var rights = await _securityData.AddRights(rightNames);
            var expectedMessage = "Something went wrong!";

            Assert.AreEqual(ActionStatus.Error, rights.Status);
            Assert.AreEqual(expectedMessage, rights.Message);
            _mock.Verify(data => data.AddRights(rightNames), () => Times.Exactly(1));
        }
    }
}