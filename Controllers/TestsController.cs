using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using KnowledgeControl.Services.Interfaces;
using KnowledgeControl.Models;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeControl.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly ITestsService _testsService;

        public TestsController(ITestsService testsService)
        {
            _testsService = testsService;
        }
        
        [HttpGet]
        public IEnumerable<TestViewModel> GetTests() =>
            _testsService.GetTests();

        [HttpGet("{id:int}")]
        public TestModel GetTest(int id) =>
            _testsService.GetTest(id);

        [HttpPost]
        public Task<TestModel> CreateTest(TestModel model) =>
            _testsService.CreateTest(model);
        
        [HttpPut]
        public Task EditTest(TestModel model) =>
            _testsService.EditTest(model);
        
        [HttpDelete("{id:int}")]
        public Task DeleteTest(int id) =>
            _testsService.DeleteTest(id);
    }
}