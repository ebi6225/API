using Microsoft.AspNetCore.Mvc;

namespace API.Shell.App_Start
{
    [ApiController]
    [Route("[controller]")]
    public class StartingApp : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "API Is Running";
        }
    }
}