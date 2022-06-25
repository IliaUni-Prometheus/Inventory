using Microsoft.AspNetCore.Mvc;

namespace Inventory.Controllers
{
    [ApiVersion("0.1")]
    [ApiVersion("0.2")]
    [Route("[controller]")]
    [ApiController]
    public class VersionController : ControllerBase
    {
        [HttpPost("apiVersion")]
        public string GetRequestedApiVersion() => "v" + HttpContext.GetRequestedApiVersion();

        [HttpGet("getVersion0_1")]
        [MapToApiVersion("0.1")]
        public string GetVersion() => "v0.1";

        [HttpGet("getVersion0_2")]
        [MapToApiVersion("0.2")]
        public string GetVersion0_2() => "v0.2";
    }
}
