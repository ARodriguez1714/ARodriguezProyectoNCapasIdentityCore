using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Dependiente : Controller
    {
        public IActionResult GetAll()
        {
            ML.Dependiente dependiente = new ML.Dependiente();

            ML.Result result = BL.Dependiente.GetAllLINQ();
            return View();
        }
    }
}
