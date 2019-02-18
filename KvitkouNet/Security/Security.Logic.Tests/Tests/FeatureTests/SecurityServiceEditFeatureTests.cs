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
using Security.Logic.Validators;

namespace Security.Logic.Tests.Tests.FeatureTests
{
    public class SecurityServiceEditFeatureTests
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
            _mock.Setup(x => x.EditFeatureRights(It.Is<int>(id =>
                    _dbFaker.Features.Any(k => k.Id.Equals(id))), It.Is<int[]>(ids =>
                    ids.All(l=>_dbFaker.AccessRights.Any(k=>k.Id == l)))))
                .Returns<int,int[]>((id, ids) =>
                {
                    return Task.FromResult(true);
                });

            //not existed feature
            _mock.Setup(x => x.EditFeatureRights(It.Is<int>(id =>
                    !_dbFaker.Features.Any(k => k.Id.Equals(id))), It.Is<int[]>(ids =>
                    ids.All(l=>_dbFaker.AccessRights.Any(k=>k.Id == l)))))
                .Returns<int,int[]>((id, ids) =>
                {
                    throw new SecurityDbException("not existed feature", ExceptionType.NotFound,
                        EntityType.Feature, new []{id.ToString()});
                });

            //not existed rights
            _mock.Setup(x => x.EditFeatureRights(It.Is<int>(id =>
                    _dbFaker.Features.Any(k => k.Id.Equals(id))), It.Is<int[]>(ids =>
                    !ids.All(l=>_dbFaker.AccessRights.Any(k=>k.Id == l)))))
                .Returns<int,int[]>((id, ids) =>
                {
                    throw new SecurityDbException("not existed rights", ExceptionType.NotFound,
                        EntityType.Right, ids.Select(l=>l.ToString()).ToArray());
                });
            
            _securityData = new FeatureService(_mock.Object, _mapper);
        }

        [Test]
        public async Task EditFeatureRights()
        {
            var id = _dbFaker.Features.FirstOrDefault().Id;
            var ids = _dbFaker.AccessRights.Take(3).Select(l=>l.Id).ToArray();

            var response = await _securityData.EditFeatureRights(id, ids);

            Assert.AreEqual(ActionStatus.Success, response.Status, response.Message);
            _mock.Verify(data => data.EditFeatureRights(id, ids), () => Times.Exactly(1));
        }

        [Test]
        public async Task EditFeatureRightsNotExistedFeature()
        {
            var id = _dbFaker.Features.Max(l=>l.Id) + 1;
            var ids = _dbFaker.AccessRights.Take(3).Select(l=>l.Id).ToArray();

            var response = await _securityData.EditFeatureRights(id, ids);
            var expectedMessage = $"Feature with id = {id} was not found";

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.EditFeatureRights(id, ids), () => Times.Exactly(1));
        }

        [Test]
        public async Task EditFeatureRightsNotExistedRights()
        {
            var id = _dbFaker.Features.FirstOrDefault().Id;
            var notExisted1 = _dbFaker.AccessRights.Max(l => l.Id) + 1;
            var notExisted2 = _dbFaker.AccessRights.Max(l => l.Id) + 2;

            var ids = new []{notExisted1, notExisted2};

            var response = await _securityData.EditFeatureRights(id, ids);
            var expectedMessage = $"Access Right with id = {string.Join(",", ids.Select(l=>l.ToString()))} was not found";

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            Assert.AreEqual(expectedMessage, response.Message);
            _mock.Verify(data => data.EditFeatureRights(id, ids), () => Times.Exactly(1));
        }
    }
}