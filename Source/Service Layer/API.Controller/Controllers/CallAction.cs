using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CallAction : ControllerBase
    {
        [HttpGet]
        [Route("TSGetRoute")]
        public string TSGetRoute()
        {
            return "D";

        }
        [HttpPost]
        [Route("TSPRoute")]
        public T PostTest()
        {
            return new T { Name = "D" };
        }

        [HttpPost]
        [Route("TSPRoute1")]
        public string PostTest1()
        {
            return "D";
        }
    }

    public class T
    {
        public string Name { get; set; }
    }
}