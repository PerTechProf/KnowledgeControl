using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using KnowledgeControl.Services.Interfaces;
using KnowledgeControl.Models;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeControl.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly ITestService _testService;

        public TestController(ITestService testService)
        {
            _testService = testService;
        }
        
        [HttpGet]
        public IEnumerable<TestViewModel> GetTests() =>
            _testService.GetTests();

        [HttpPost]
        public Task CreateTest(TestModel model) =>
            _testService.CreateTest(model);
        
        [HttpPut]
        public Task EditTest(TestModel model) =>
            _testService.EditTest(model);
        
        [HttpDelete]
        public Task DeleteTest(TestModel model) =>
            _testService.DeleteTest(model);
    }
}