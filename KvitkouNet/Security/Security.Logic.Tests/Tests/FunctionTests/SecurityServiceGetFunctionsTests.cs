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

namespace Security.Logic.Tests.Tests.FunctionTests
{
    public class SecurityServiceGetFunctionsTests
    {
        private IFunctionService _securityData;
        private SecurityDbFaker _dbFaker;
        private IMapper _mapper;
        private Mock<ISecurityData> _mock;

        [SetUp]
        public void Setup()
        {
            _dbFaker = new SecurityDbFaker();
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccessFunctionProfile>();
            }));

            _mock = new Mock<ISecurityData>();
            _mock.Setup(x => x.GetFunctions(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns((int i, int p, string m) =>
                    Task.FromResult(new AccessFunctionsGetResult{
                        Functions = _dbFaker.Functions.Where(l => string.IsNullOrEmpty(m) || l.Name.Contains(m))
                        .OrderBy(l => l.Name).Skip((p - 1) * i).Take(i)}));
            _securityData = new FunctionService(_mock.Object, _mapper);
        }
        
        [Test]
        public async Task GetFunctions()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;

            var response = await _securityData.GetFunctions(itemsPerPage, pageNumber);
            var expected = _mapper.Map<AccessFunction[]>(_dbFaker.Functions
                .OrderBy(l => l.Name).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToArray());

            CollectionAssert.AreEqual(expected, response.AccessFunctions, new FunctionComparer());
            _mock.Verify(
                data => data.GetFunctions(itemsPerPage, pageNumber, ""), () => Times.Exactly(1));
        }

        [Test]
        public async Task GetFunctionsMask()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;
            var mask = "no";

            var response = (await _securityData.GetFunctions(itemsPerPage, pageNumber, mask)).AccessFunctions;

            var expected = _mapper.Map<AccessFunction[]>(_dbFaker.Functions.Where(l => string.IsNullOrEmpty(mask) || l.Name.Contains(mask))
                .OrderBy(l => l.Name).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToArray());

            CollectionAssert.AreEqual(expected, response, new FunctionComparer());
            _mock.Verify(
                data => data.GetFunctions(It.Is<int>(i => i == itemsPerPage), It.Is<int>(i => i == pageNumber),
                    It.Is<string>(i => i == mask)), () => Times.Exactly(1));
        }

        [Test]
        public async Task GetFunctionsZeroItemsPerPage()
        {
            var itemsPerPage = 0;
            var pageNumber = 1;

            var response = (await _securityData.GetFunctions(itemsPerPage, pageNumber));

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.AccessFunctions, new FunctionComparer());
            _mock.Verify(data => data.GetFunctions(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetFunctionsNegativeItemsPerPage()
        {
            var itemsPerPage = -10;
            var pageNumber = 1;

            var response = (await _securityData.GetFunctions(itemsPerPage, pageNumber));

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.AccessFunctions, new FunctionComparer());
            _mock.Verify(data => data.GetFunctions(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetFunctionsZeroPage()
        {
            var itemsPerPage = 10;
            var pageNumber = 0;

            var response = (await _securityData.GetFunctions(itemsPerPage, pageNumber));

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.AccessFunctions, new FunctionComparer());
            _mock.Verify(data => data.GetFunctions(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetFunctionsNegativePage()
        {
            var itemsPerPage = 10;
            var pageNumber = -1;

            var response = (await _securityData.GetFunctions(itemsPerPage, pageNumber));

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.AccessFunctions, new FunctionComparer());
            _mock.Verify(data => data.GetFunctions(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetFunctionsToLongMask()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;
            var mask = "no423534645674tgdfbdfmgvbngdnty356y34gvt634fredgdfhnfgmytngv56t43c5t23rhfghnfgjgdfbdfmgvbngdnty356y34gvt634fredgdfhnfgmytngv56t43c5t23rhfghnfgj";

            var response = (await _securityData.GetFunctions(itemsPerPage, pageNumber, mask));

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.AccessFunctions, new FunctionComparer());
            _mock.Verify(data => data.GetFunctions(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetFunctionsMaskStartsEndsWithSpaces()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;
            var mask = " no ";

            var response = (await _securityData.GetFunctions(itemsPerPage, pageNumber, mask)).AccessFunctions;

            var expected = _mapper.Map<AccessFunction[]>(_dbFaker.Functions.Where(l => string.IsNullOrEmpty(mask) || l.Name.Contains(mask.Trim()))
                .OrderBy(l => l.Name).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToArray());

            CollectionAssert.AreEqual(expected, response, new FunctionComparer());
            _mock.Verify(data => data.GetFunctions(It.Is<int>(i => i == itemsPerPage), It.Is<int>(i => i == pageNumber),
                    It.Is<string>(i => i == mask.Trim())), () => Times.Exactly(1));
        }
    }
}