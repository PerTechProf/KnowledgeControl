using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using KnowledgeControl.Entities;
using KnowledgeControl.Services.Interfaces;
using KnowledgeControl.Services;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeControl.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class SolutionController : ControllerBase
    {
        private readonly ISolutionService _solutionService;

        public SolutionController(ISolutionService solutionService)
        {
            _solutionService = solutionService;
        }

        public IEnumerable<Solution> GetSolutions() =>
            _solutionService.GetSolutions();

        public Task PostSolution(Solution model) =>
            _solutionService.PostSolution(model);
    }
}