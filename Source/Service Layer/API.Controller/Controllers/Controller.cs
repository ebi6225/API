using API.Controller.Controllers;
using API.Controller.Services;
using API.Model.Requests;
using API.Model.Responses;
using Base.Securit.Web.API.Validation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class Controller : AbstractController
    {
        [HttpGet]
        [Route("TSGetRoute")]
        [APIAuthorize]
        public string TSGetRoute()
        {
            return "D";
        }
        [HttpPost]        
        [Route("email")]
        [APIAuthorize]
        public SampleResponse email(SampleRequest request)
        {
            return new MatchineNameService().GetMachineName(null);
        }

        [HttpPost]
        [Route("TSPRoute1")]
        public string PostTest1()
        {
            return "D";
        }
    }
}