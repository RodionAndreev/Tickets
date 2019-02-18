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

namespace Security.Logic.Tests.Tests.RoleTests
{
    public class SecurityServiceGetRolesTests
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
                cfg.AddProfile<RoleProfile>();
            }));

            _mock = new Mock<ISecurityData>();
            _mock.Setup(x => x.GetRoles(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()))
                .Returns((int i, int p, string m) =>
                    Task.FromResult(new RolesGetResult{Roles = _dbFaker.Roles.Where(l => string.IsNullOrEmpty(m) || l.Name.Contains(m))
                        .OrderBy(l => l.Name).Skip((p - 1) * i).Take(i)}));
            _securityData = new RoleService(_mock.Object, _mapper);
        }
        
        [Test]
        public async Task GetRoles()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;

            var response = await _securityData.GetRoles(itemsPerPage, pageNumber);
            var expected = _mapper.Map<Role[]>(_dbFaker.Roles
                .OrderBy(l => l.Name).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToArray());

            CollectionAssert.AreEqual(expected, response.Roles, new RoleComparer());
            _mock.Verify(data => data.GetRoles(itemsPerPage, pageNumber, ""), () => Times.Exactly(1));
        }

        [Test]
        public async Task GetRolesMask()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;
            var mask = "no";

            var response = (await _securityData.GetRoles(itemsPerPage, pageNumber, mask)).Roles;

            var expected = _mapper.Map<Role[]>(_dbFaker.Roles.Where(l => string.IsNullOrEmpty(mask) || l.Name.Contains(mask))
                .OrderBy(l => l.Name).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToArray());

            CollectionAssert.AreEqual(expected, response, new RoleComparer());
            _mock.Verify(
                data => data.GetRoles(It.Is<int>(i => i == itemsPerPage), It.Is<int>(i => i == pageNumber),
                    It.Is<string>(i => i == mask)), () => Times.Exactly(1));
        }

        [Test]
        public async Task GetRolesZeroItemsPerPage()
        {
            var itemsPerPage = 0;
            var pageNumber = 1;

            var response = (await _securityData.GetRoles(itemsPerPage, pageNumber));

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.Roles, new RoleComparer());
            _mock.Verify(data => data.GetRoles(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetRolesNegativeItemsPerPage()
        {
            var itemsPerPage = -10;
            var pageNumber = 1;

            var response = (await _securityData.GetRoles(itemsPerPage, pageNumber));

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.Roles, new RoleComparer());
            _mock.Verify(data => data.GetRoles(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetRolesZeroPage()
        {
            var itemsPerPage = 10;
            var pageNumber = 0;

            var response = (await _securityData.GetRoles(itemsPerPage, pageNumber));

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.Roles, new RoleComparer());
            _mock.Verify(data => data.GetRoles(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetRolesNegativePage()
        {
            var itemsPerPage = 10;
            var pageNumber = -1;

            var response = (await _securityData.GetRoles(itemsPerPage, pageNumber));

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.Roles, new RoleComparer());
            _mock.Verify(data => data.GetRoles(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetRolesToLongMask()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;
            var mask = "no423534645674tgdfbdfmgvbngdnty356y34gvt634fredgdfhnfgmytngv56t43c5t23rhfghnfgjgdfbdfmgvbngdnty356y34gvt634fredgdfhnfgmytngv56t43c5t23rhfghnfgj";

            var response = (await _securityData.GetRoles(itemsPerPage, pageNumber, mask));

            Assert.AreEqual(ActionStatus.Warning, response.Status);
            CollectionAssert.AreEqual(null, response.Roles, new RoleComparer());
            _mock.Verify(data => data.GetRoles(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>()), () => Times.Exactly(0));
        }

        [Test]
        public async Task GetRolesMaskStartsEndsWithSpaces()
        {
            var itemsPerPage = 10;
            var pageNumber = 1;
            var mask = " no ";

            var response = (await _securityData.GetRoles(itemsPerPage, pageNumber, mask)).Roles;

            var expected = _mapper.Map<Role[]>(_dbFaker.Roles.Where(l => string.IsNullOrEmpty(mask) || l.Name.Contains(mask.Trim()))
                .OrderBy(l => l.Name).Skip((pageNumber - 1) * itemsPerPage).Take(itemsPerPage).ToArray());

            CollectionAssert.AreEqual(expected, response, new RoleComparer());
            _mock.Verify(data => data.GetRoles(It.Is<int>(i => i == itemsPerPage), It.Is<int>(i => i == pageNumber),
                    It.Is<string>(i => i == mask.Trim())), () => Times.Exactly(1));
        }
    }
}