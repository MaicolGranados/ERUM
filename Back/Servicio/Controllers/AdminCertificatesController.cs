using Microsoft.AspNetCore.Mvc;

namespace AcreditacionErumApi.Controllers
{
    public class AdminCertificatesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
