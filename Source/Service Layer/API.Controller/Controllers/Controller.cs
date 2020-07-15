using API.Model.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("CallAction")]
    public class Controller : ControllerBase
    {
        [HttpGet]
        [Route("TSGetRoute")]
        public string TSGetRoute()
        {
            return "D";
        }
        [HttpPost]
        [Route("TSPRoute")]
        public SampleResponse PostTest()
        {
            return new SampleResponse { Name = "D" };
        }

        [HttpPost]
        [Route("TSPRoute1")]
        public string PostTest1()
        {
            return "D";
        }
    }
}