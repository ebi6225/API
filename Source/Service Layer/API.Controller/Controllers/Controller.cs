using API.Controller.Services;
using API.Model.Requests;
using API.Model.Responses;
using Base.Securit.Web.API.Validation;
using Base.Web.API.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class Controller : BaseAbstractControllerBase
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
        public object email(SampleRequest request)
        {
            return new ContactInfoService().DoProcessBase(request);
        }
        [HttpPost]
        [Route("TSPRoute1")]
        public string PostTest1()
        {
            return "D";
        }
    }
}