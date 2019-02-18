using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using NUnit.Framework;
using Security.Data;
using Security.Data.Models;
using Security.Logic.Implementations;
using Security.Logic.MappingProfiles;
using Security.Logic.Models;
using Security.Logic.Models.Enums;
using Security.Logic.Services;
using Security.Logic.Tests.Comparers;
using Security.Logic.Tests.Fakers;
using Security.Logic.Validators;

namespace Security.Logic.Tests.Tests.FeatureTests
{
    public class SecurityServiceGetFeatureTests
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
                cfg.AddProfile<FeatureProfile>();
            }));

            _mock = new Mock<ISecurityData>();
            _mock.Setup(x => x.GetFeatures(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns((int i, int p, string m) =>
                    Task.FromResult(new FeaturesGetResult{
                        Features = _dbFaker.Features.Where(l => string.IsNullOrEmpty(m) || l.Name.Contains(m))
                        .OrderBy(l => l.Name).Skip((p - 1) * i).Take(i)}));
            _securityData = new FeatureService(_mock.Object, _mapper);
        }
        
        [Test]
        public async Task GetFeatures()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;

            var response = await _securityData.GetFeatures(itemsPerPage, pageNumber);
            var expected = _mapper.Map<Feature[]>(_dbFaker.Features
                .OrderBy(l => l.Name).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToArray());

            CollectionAssert.AreEqual(expected, response.Features, new FeatureComparer());
            _mock.Verify(data => data.GetFeatures(itemsPerPage, pageNumber, ""), () => Times.Exactly(1));
        }

        [Test]
        public async Task GetFeaturesMask()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;
            var mask = "no";

            var response = (await _securityData.GetFeatures(itemsPerPage, pageNumber, mask)).Features;

            var expected = _mapper.Map<Feature[]>(_dbFaker.Features.Where(l => string.IsNullOrEmpty(mask) || l.Name.Contains(mask))
                .OrderBy(l => l.Name).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToArray());

            CollectionAssert.AreEqual(expected, response, new FeatureComparer());
            _mock.Verify(
                data => data.GetFeatures(It.Is<int>(i => i == itemsPerPage), It.Is<int>(i => i == pageNumber),
                    It.Is<string>(i => i == mask)), () => Times.Exactly(1));
        }

        [Test]
        public async Task GetFeaturesZeroItemsPerPage()
        {
            var itemsPerPage = 0;
            var pageNumber = 1;

            var response = await _securityData.GetFeatures(itemsPerPage, pageNumber);
            
            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.Features, new FeatureComparer());
            _mock.Verify(data => data.GetFeatures(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetFeaturesNegativeItemsPerPage()
        {
            var itemsPerPage = -10;
            var pageNumber = 1;

            var response = await _securityData.GetFeatures(itemsPerPage, pageNumber);

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.Features, new FeatureComparer());
            _mock.Verify(data => data.GetFeatures(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetFeaturesZeroPage()
        {
            var itemsPerPage = 10;
            var pageNumber = 0;

            var response = await _securityData.GetFeatures(itemsPerPage, pageNumber);
            
            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.Features, new FeatureComparer());
            _mock.Verify(data => data.GetFeatures(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetFeaturesNegativePage()
        {
            var itemsPerPage = 10;
            var pageNumber = -1;

            var response = await _securityData.GetFeatures(itemsPerPage, pageNumber);
            
            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.Features, new FeatureComparer());
            _mock.Verify(data => data.GetFeatures(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetFeaturesToLongMask()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;
            var mask = "no423534645674tgdfbdfmgvbngdnty356y34gvt634fredgdfhnfgmytngv56t43c5t23rhfghnfgjgdfbdfmgvbngdnty356y34gvt634fredgdfhnfgmytngv56t43c5t23rhfghnfgj";

            var response = (await _securityData.GetFeatures(itemsPerPage, pageNumber, mask));
            
            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.Features, new FeatureComparer());
            _mock.Verify(data => data.GetFeatures(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetFeaturesMaskStartsEndsWithSpaces()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;
            var mask = " no ";

            var response = (await _securityData.GetFeatures(itemsPerPage, pageNumber, mask)).Features;

            var expected = _mapper.Map<Feature[]>(_dbFaker.Features.Where(l => string.IsNullOrEmpty(mask) || l.Name.Contains(mask.Trim()))
                .OrderBy(l => l.Name).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToArray());

            CollectionAssert.AreEqual(expected, response, new FeatureComparer());
            _mock.Verify(data => data.GetFeatures(It.Is<int>(i => i == itemsPerPage), It.Is<int>(i => i == pageNumber),
                    It.Is<string>(i => i == mask.Trim())), () => Times.Exactly(1));
        }
    }
}