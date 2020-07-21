using API.Controller.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace API.Shell.App_Start
{
    public class CallAction : AbstractController
    {
        [HttpGet]
        public string Get()
        {
            return "API Is Running";
        }
    }
}