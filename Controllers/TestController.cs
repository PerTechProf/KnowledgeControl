using KnowledgeControl.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KnowledgeControl.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class TestController : ControllerBase
    {
        private readonly IAuthService _auth;

        public TestController(IAuthService auth)
        {
            _auth = auth;
        }
        
        [HttpGet]
        public void GetTests()
        {
            
        }
    }
}