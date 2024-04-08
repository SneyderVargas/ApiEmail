using Microsoft.AspNetCore.Mvc;

namespace ApiEmail.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
