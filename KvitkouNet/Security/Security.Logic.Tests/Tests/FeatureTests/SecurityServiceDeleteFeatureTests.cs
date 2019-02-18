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
    public class SecurityServiceDeleteFeatureTests
    {
        private IFeatureService _securityData;
        private IMapper _mapper;
        private Mock<ISecurityData> _mock;
        private SecurityDbFaker _dbFaker;

        [SetUp]
        public void Setup()
        {
            _dbFaker = new SecurityDbFaker();
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<FeatureProfile>();
            }));

            _mock = new Mock<ISecurityData>();

            //success
            _mock.Setup(x => x.DeleteFeature(It.Is<int>(id =>
                    _dbFaker.Features.Any(l => l.Id == id))))
                .Returns<int>(id =>
                {
                    return Task.FromResult(true);
                });

            //not exists
            _mock.Setup(x => x.DeleteFeature(It.Is<int>(id =>
                    _dbFaker.Features.All(l => l.Id != id))))
                .Returns<int>(id =>
                {
                    throw new SecurityDbException("not exists", ExceptionType.NotFound,
                        EntityType.Feature, new[] { id.ToString() });
                });


            _securityData = new FeatureService(_mock.Object, _mapper);
        }

        [Test]
        public async Task DeleteFeatureNotExisted()
        {
            var id = _dbFaker.Features.Max(l => l.Id) + 1;

            var result = await _securityData.DeleteFeature(id);
            var expectedMessage = $"Feature with id = {id} was not found";

            Assert.AreEqual(ActionStatus.Warning, result.Status);
            Assert.AreEqual(expectedMessage, result.Message);
            _mock.Verify(data => data.DeleteFeature(It.Is<int>(db => db == 0 )), () => Times.Exactly(0));
        }

        [Test]
        public async Task DeleteFeature()
        {
            var id = _dbFaker.Features.Max(l => l.Id);

            var result = await _securityData.DeleteFeature(id);

            Assert.AreEqual(ActionStatus.Success, result.Status);
            _mock.Verify(data => data.DeleteFeature(It.Is<int>(db => db == id )), () => Times.Exactly(1));
        }
    }
}