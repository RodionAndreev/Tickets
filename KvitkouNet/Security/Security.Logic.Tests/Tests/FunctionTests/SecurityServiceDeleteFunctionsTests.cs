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

namespace Security.Logic.Tests.Tests.FunctionTests
{
    public class SecurityServiceDeleteFunctionsTests
    {
        private IFunctionService _securityData;
        private IMapper _mapper;
        private Mock<ISecurityData> _mock;
        private SecurityDbFaker _dbFaker;

        [SetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AccessRightProfile>();
                cfg.AddProfile<AccessFunctionProfile>();
            }));

            _dbFaker = new SecurityDbFaker();

            _mock = new Mock<ISecurityData>();

            //success
            _mock.Setup(x => x.DeleteFunction(It.Is<int>(id =>
                    _dbFaker.Functions.Any(l => l.Id == id))))
                .Returns<int>(id =>
                {
                    return Task.FromResult(true);
                });

            //not exists
            _mock.Setup(x => x.DeleteFunction(It.Is<int>(id =>
                    _dbFaker.Functions.All(l => l.Id != id))))
                .Returns<int>(id =>
                {
                    throw new SecurityDbException("not exists", ExceptionType.NotFound,
                        EntityType.Function, new[] { id.ToString() });
                });

            _securityData = new FunctionService(_mock.Object, _mapper);
        }

        [Test]
        public async Task DeleteFunctionNotExisted()
        {
            var id = _dbFaker.Functions.Max(l => l.Id) + 1;

            var result = await _securityData.DeleteFunction(id);
            var expectedMessage = $"Access Function with id = {id} was not found";

            Assert.AreEqual(ActionStatus.Warning, result.Status);
            Assert.AreEqual(expectedMessage, result.Message);
            _mock.Verify(data => data.DeleteFunction(It.Is<int>(db => db == 0 )), () => Times.Exactly(0));
        }

        [Test]
        public async Task DeleteFunction()
        {
            var id = _dbFaker.Functions.Max(l => l.Id);

            var result = await _securityData.DeleteFunction(id);

            Assert.AreEqual(ActionStatus.Success, result.Status);
            _mock.Verify(data => data.DeleteFunction(It.Is<int>(db => db == id )), () => Times.Exactly(1));
        }
    }
}