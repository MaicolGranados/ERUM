using Microsoft.AspNetCore.Mvc;

namespace AcreditacionErumApi.Controllers
{
    public class AdminTemplatesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
