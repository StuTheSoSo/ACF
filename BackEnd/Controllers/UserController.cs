using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
