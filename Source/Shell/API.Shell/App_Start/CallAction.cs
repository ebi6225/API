using API.Controller.Controllers;
using Base.Web.API.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace API.Shell.App_Start
{
    public class CallAction : AbstractController
    {
        public CallAction(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }


        [HttpGet]
        public string Get()
        {
            return AppSettingManager.Instance.Get<string>("APIMessage");
        }
    }
}