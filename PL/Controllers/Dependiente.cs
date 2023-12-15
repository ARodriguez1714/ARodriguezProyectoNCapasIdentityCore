using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Dependiente : Controller
    {
        public IActionResult GetAll()
        {
            return View();
        }
    }
}
