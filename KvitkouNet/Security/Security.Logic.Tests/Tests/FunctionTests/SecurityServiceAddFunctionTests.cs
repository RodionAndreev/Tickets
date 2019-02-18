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

namespace Security.Logic.Tests.Tests.FunctionTests
{
    public class SecurityServiceAddFunctionTests
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
                cfg.AddProfile<AccessRightProfile>();
                cfg.AddProfile<AccessFunctionProfile>();
            }));

            _mock = new Mock<ISecurityData>();

            //success
            _mock.Setup(x => x.AddFunction(It.Is<string>(name => 
                    !string.IsNullOrEmpty(name) 
                    && !_dbFaker.Functions.Any(l=>l.Name.Equals(name))), It.Is<int>(featureId => 
                    _dbFaker.Features.Any(l=>l.Id == featureId))))
                .Returns(() => Task.FromResult(_dbFaker.Functions.Max(l=>l.Id) + 1));
            
            //name existed
            _mock.Setup(x => x.AddFunction(It.Is<string>(name => 
                    !string.IsNullOrEmpty(name) 
                    && _dbFaker.Functions.Any(l=>l.Name.Equals(name))), It.Is<int>(featureId => 
                    _dbFaker.Features.Any(l=>l.Id == featureId))))
                .Returns<string, int>((name, id) =>
                {
                    throw new SecurityDbException("name existed", ExceptionType.NameExists,
                        EntityType.Function, new[] {name});
                });

            //feature not exists
            _mock.Setup(x => x.AddFunction(It.Is<string>(name => 
                    !string.IsNullOrEmpty(name) 
                    && !_dbFaker.Functions.Any(l=>l.Name.Equals(name))), It.Is<int>(featureId => 
                    _dbFaker.Features.All(l => l.Id != featureId))))
                .Returns<string, int>((name, id) =>
                {
                    throw new SecurityDbException("feature not exists", ExceptionType.NotFound,
                        EntityType.Feature, new[] {id.ToString()});
                });

            //feature not exists
            _mock.Setup(x => x.AddFunction(It.Is<string>(name => 
                    name.Equals("Error!")), It.IsAny<int>()))
                .Returns<string, int>((name, id) =>
                {
                    throw new Exception();
                });
            
            _securityData = new FunctionService(_mock.Object, _mapper);
        }

        [Test]
        public async Task AddFunction()
        {
            var featureId = _dbFaker.Features.FirstOrDefault()?.Id ?? 0;
            var functionName = "NormalName";

            var functions = await _securityData.AddFunction(functionName, featureId);
            var expected = _dbFaker.Functions.Max(l => l.Id) + 1;

            Assert.AreEqual(ActionStatus.Success, functions.Status);
            Assert.AreEqual(expected, functions.Id);
            _mock.Verify(data => data.AddFunction(functionName, featureId), () => Times.Exactly(1));
        }

        [Test]
        public async Task AddFunctionExistedName()
        {
            var existedName = _dbFaker.Functions.FirstOrDefault()?.Name;
            var featureId = _dbFaker.Features.FirstOrDefault()?.Id ?? 0;
            
            var functions = await _securityData.AddFunction(existedName, featureId);
            var expectedMessage = $"Names: {existedName} of Access Function already exist";

            Assert.AreEqual(ActionStatus.Warning, functions.Status);
            Assert.AreEqual(expectedMessage, functions.Message);
            _mock.Verify(data => data.AddFunction(existedName, featureId), () => Times.Exactly(1));
        }

        [Test]
        public async Task AddFunctionToLongName()
        {
            var featureId = _dbFaker.Features.FirstOrDefault()?.Id ?? 0;
            var functionName =
                "In the fields of physical security and information security, access control(AC) is the selective restriction of access to a place or other resource";
                
            var functions = await _securityData.AddFunction(functionName, featureId);
            var expectedMessage = "Name must be between 1 and 100 characters";

            Assert.AreEqual(ActionStatus.Warning, functions.Status);
            Assert.AreEqual(expectedMessage, functions.Message);
            _mock.Verify(data => data.AddFunction(functionName, featureId), () => Times.Exactly(0));
        }

        [Test]
        public async Task AddFunctionUnknownError()
        {
            var featureId = _dbFaker.Features.FirstOrDefault()?.Id ?? 0;
            var functionName = "Error!";

            var functions = await _securityData.AddFunction(functionName, featureId);
            var expectedMessage = "Something went wrong!";

            Assert.AreEqual(ActionStatus.Error, functions.Status);
            Assert.AreEqual(expectedMessage, functions.Message);
            _mock.Verify(data => data.AddFunction(functionName, featureId), () => Times.Exactly(1));
        }

        [Test]
        public async Task AddFunctionNotExistingFeature()
        {
            var featureId = 20000;
            var functionName = "NormalName";

            var functions = await _securityData.AddFunction(functionName, featureId);
            var expectedMessage = $"Feature with id = {featureId.ToString()} was not found";

            Assert.AreEqual(ActionStatus.Warning, functions.Status);
            Assert.AreEqual(expectedMessage, functions.Message);
            _mock.Verify(data => data.AddFunction(functionName, featureId), () => Times.Exactly(1));
        }
    }
}