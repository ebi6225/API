using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [EnableCors("AllowOrigin")]
    public class CallAction : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "API Is Running";
        }

        [HttpPost]
        [Route("TSPRoute")]
        public string PostTest()
        {
            return "D";
        }

        [HttpPost]
        [Route("TSPRoute1")]
        public string PostTest1()
        {
            return "D";
        }
    }
}