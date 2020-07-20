using API.Controller.Services;
using API.Model.Requests;
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
        [Route("email")]
        public SampleResponse email(SampleRequest request)
        {
            return new MatchineNameService().GetMachineName(request);
        }

        [HttpPost]
        [Route("TSPRoute1")]
        public string PostTest1()
        {
            return "D";
        }
    }
}