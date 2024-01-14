using Microsoft.AspNetCore.Mvc;

namespace Konusarak_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from SampleController!");
        }
    }
}
