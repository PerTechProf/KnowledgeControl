using System.Threading.Tasks;
using KnowledgeControl.Entities;
using KnowledgeControl.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeControl.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ResultController : ControllerBase
    {
        private readonly IResultService _resultService;

        public ResultController(IResultService resultService)
        {
            _resultService = resultService;
        }

        [HttpGet("{testId:int}")]
        public Task<Result> GetResult(int testId) =>
            _resultService.GetResult(testId);
    }
}