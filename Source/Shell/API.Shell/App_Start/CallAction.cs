using API.Controller.Controllers;
using Base.Web.API.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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
            string connectionString = Configuration.GetConnectionString("Name");

            return AppSettingManager.Instance.Get<string>("APIMessage");
        }
    }
}