using System.Collections.Generic;
using System.Threading.Tasks;
using KnowledgeControl.Entities;
using KnowledgeControl.Models;
using KnowledgeControl.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeControl.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly IResultsService _resultsService;

        public ResultsController(IResultsService resultsService)
        {
            _resultsService = resultsService;
        }

        [HttpGet("{testId:int}")]
        public CertificateModel GetCertificate(int testId) =>
            _resultsService.GetCertificate(testId);
    }
}