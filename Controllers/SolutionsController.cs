using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using KnowledgeControl.Entities;
using KnowledgeControl.Models;
using KnowledgeControl.Services.Interfaces;
using KnowledgeControl.Services;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeControl.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolutionsController : ControllerBase
    {
        private readonly ISolutionsService _solutionsService;

        public SolutionsController(ISolutionsService solutionsService)
        {
            _solutionsService = solutionsService;
        }
        
        [HttpGet("{testId:int}")]
        public IEnumerable<Solution> GetSolutions(int testId) =>
            _solutionsService.GetSolutions(testId);
        
        [HttpPost]
        public Task PostSolution(PostSolutionModel model) =>
            _solutionsService.PostSolution(model);
    }
}