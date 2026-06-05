using Microsoft.AspNetCore.Mvc;

namespace AcreditacionErumApi.Controllers
{
    public class StatusController : Controller
    {
        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(new { status = "API is running" });
        }
    }
}
