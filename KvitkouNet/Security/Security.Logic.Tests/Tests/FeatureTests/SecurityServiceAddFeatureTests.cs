using System;
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

namespace Security.Logic.Tests.Tests.FeatureTests
{
    public class SecurityServiceAddFeatureTests
    {
        private IFeatureService _securityData;
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
            _mock.Setup(x => x.AddFeature(It.Is<string>(l =>
                    !_dbFaker.Features.Any(k => k.Name.Equals(l)))))
                .Returns(() => Task.FromResult(
                    _dbFaker.Features.Max(l => l.Id) + 1));

            //existed name
            _mock.Setup(x => x.AddFeature(It.Is<string>(l => 
                    _dbFaker.Features.Any(k=>k.Name.Equals(l)))))
                .Returns<string>(name =>
                {
                    throw new SecurityDbException("existed name", ExceptionType.NameExists,
                        EntityType.Feature, new []{name});
                });

            //error
            _mock.Setup(x => x.AddFeature(It.Is<string>(l => 
                    l.Equals("Error!"))))
                .Returns<string>(name =>
                {
                    throw new Exception();
                });

            _securityData = new FeatureService(_mock.Object, _mapper);
        }

        [Test]
        public async Task AddFeature()
        {
            var featureName = "NormalName";

            var features = await _securityData.AddFeature(featureName);
            var expected = _dbFaker.Features.Max(l => l.Id) + 1;

            Assert.AreEqual(ActionStatus.Success, features.Status);
            Assert.AreEqual(expected, features.Id);
            _mock.Verify(data => data.AddFeature(featureName), () => Times.Exactly(1));
        }

        [Test]
        public async Task AddFeatureExistedName()
        {
            var existedName = _dbFaker.Features.FirstOrDefault()?.Name;

            var features = await _securityData.AddFeature(existedName);
            var expectedMessage = $"Names: {existedName} of Feature already exist";
            
            Assert.AreEqual(ActionStatus.Warning, features.Status);
            Assert.AreEqual(expectedMessage, features.Message);
            _mock.Verify(data => data.AddFeature(existedName), () => Times.Exactly(1));
        }

        [Test]
        public async Task AddFeatureToLongName()
        {
            var featureName = "In the fields of physical security and information security, access control(AC) is the selective restriction of access to a place or other resource";

            var features = await _securityData.AddFeature(featureName);
            var expectedMessage = "Name must be between 1 and 100 characters";

            Assert.AreEqual(ActionStatus.Warning, features.Status);
            Assert.AreEqual(expectedMessage, features.Message);
            _mock.Verify(data => data.AddFeature(featureName), () => Times.Exactly(0));
        }

        [Test]
        public async Task AddFeatureUnknownError()
        {
            var featureName = "Error!";

            var features = await _securityData.AddFeature(featureName);
            var expectedMessage = "Something went wrong!";

            Assert.AreEqual(ActionStatus.Error, features.Status);
            Assert.AreEqual(expectedMessage, features.Message);
            _mock.Verify(data => data.AddFeature(featureName), () => Times.Exactly(1));
        }
    }
}