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

namespace Security.Logic.Tests.Tests.AccessRightTests
{
    public class SecurityServiceGetRightsTests
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
            _mock.Setup(x => x.GetRights(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns((int i, int p, string m) =>
                    Task.FromResult(new AccessRightsGetResult{Rights = _dbFaker.AccessRights.Where(l=> string.IsNullOrEmpty(m) || l.Name.Contains(m))
                        .OrderBy(l=>l.Name).Skip((p-1)*i).Take(i)}));
            _securityData = new RightsService(_mock.Object, _mapper);
        }

        [Test]
        public async Task GetRights()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;

            var response = (await _securityData.GetRights(itemsPerPage, pageNumber)).AccessRights;
            var expected = _mapper.Map<AccessRight[]>(_dbFaker.AccessRights
                .OrderBy(l => l.Name).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToArray());

            CollectionAssert.AreEqual(expected, response, new AccessRightComparer());
            _mock.Verify(data => data.GetRights(itemsPerPage, pageNumber, ""), () => Times.Exactly(1));
        }
        [Test]
        public async Task GetRightsMask()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;
            var mask = "no";

            var response = (await _securityData.GetRights(itemsPerPage, pageNumber, mask)).AccessRights;

            var expected = _mapper.Map<AccessRight[]>(_dbFaker.AccessRights.Where(l => string.IsNullOrEmpty(mask) || l.Name.Contains(mask))
                .OrderBy(l => l.Name).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToArray());
            
            CollectionAssert.AreEqual(expected, response, new AccessRightComparer());
            _mock.Verify(
                data => data.GetRights(It.Is<int>(i => i == itemsPerPage), It.Is<int>(i => i == pageNumber),
                    It.Is<string>(i => i == mask)), () => Times.Exactly(1));
        }

        [Test]
        public async Task GetRightsZeroItemsPerPage()
        {
            var itemsPerPage = 0;
            var pageNumber = 1;

            var response = (await _securityData.GetRights(itemsPerPage, pageNumber));

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.AccessRights, new AccessRightComparer());
            _mock.Verify(data => data.GetRights(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetRightsNegativeItemsPerPage()
        {
            var itemsPerPage = -10;
            var pageNumber = 1;

            var response = (await _securityData.GetRights(itemsPerPage, pageNumber));
            
            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.AccessRights, new AccessRightComparer());
            _mock.Verify(data => data.GetRights(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetRightsZeroPage()
        {
            var itemsPerPage = 10;
            var pageNumber = 0;

            var response = await _securityData.GetRights(itemsPerPage, pageNumber);

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.AccessRights, new AccessRightComparer());
            _mock.Verify(data => data.GetRights(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetRightsNegativePage()
        {
            var itemsPerPage = 10;
            var pageNumber = -1;

            var response = (await _securityData.GetRights(itemsPerPage, pageNumber));

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.AccessRights, new AccessRightComparer());
            _mock.Verify(data => data.GetRights(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetRightsToLongMask()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;
            var mask = "no423534645674tgdfbdfmgvbngdnty356y34gvt634fredgdfhnfgmytngv56t43c5t23rhfghnfgjgdfbdfmgvbngdnty356y34gvt634fredgdfhnfgmytngv56t43c5t23rhfghnfgj";

            var response = (await _securityData.GetRights(itemsPerPage, pageNumber, mask));
            
            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.AccessRights, new AccessRightComparer());
            _mock.Verify(data => data.GetRights(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetRightsMaskStartsEndsWithSpaces()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;
            var mask = " no ";

            var response = (await _securityData.GetRights(itemsPerPage, pageNumber, mask)).AccessRights;

            var expected = _mapper.Map<AccessRight[]>(_dbFaker.AccessRights.Where(l => string.IsNullOrEmpty(mask) || l.Name.Contains(mask.Trim()))
                .OrderBy(l => l.Name).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToArray());

            CollectionAssert.AreEqual(expected, response, new AccessRightComparer());
            _mock.Verify(data => data.GetRights(It.Is<int>(i => i == itemsPerPage), It.Is<int>(i => i == pageNumber),
                    It.Is<string>(i => i == mask.Trim())), () => Times.Exactly(1));
        }
    }
}