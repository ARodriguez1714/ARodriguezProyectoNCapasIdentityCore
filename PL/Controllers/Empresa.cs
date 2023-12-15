using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class Empresa : Controller
    {
        public IActionResult GetAll()
        {

            ML.Empresa empresa = new ML.Empresa();
            ML.Result result = BL.Empresa.GetAll();

            if (result.Correct)
            {
                empresa.Empresas = result.Objects;
                return View(empresa);
            }
            else
            {
                return View(empresa);
            }
        }


        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Form(int? IdEmpresa)
        {
            ML.Empresa empresa = new ML.Empresa();
            if (IdEmpresa == null)
            {
                //add
                ViewBag.Titulo = "Agregar Empresa";
                ViewBag.Boton = "Agregar";
                return View(empresa);
            }
            else
            {
                //update
                ML.Result result = BL.Empresa.GetById(IdEmpresa);

                if (result.Correct)
                {
                    empresa = (ML.Empresa)result.Object;
                    ViewBag.Titulo = "Modificar Empresa";
                    ViewBag.Boton = "Modificar";
                    return View(empresa);
                }
                else
                {
                    ViewBag.Titulo = "Error";
                    ViewBag.Alert = "danger";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Form(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            if (empresa.IdEmpresa == null)
            {
                result = BL.Empresa.Add(empresa);

                if (result.Correct)
                {
                    ViewBag.Titulo = "Correcto";
                    ViewBag.Alert = "success";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "Error";
                    ViewBag.Alert = "danger";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
            }
            else
            {
                result = BL.Empresa.Update(empresa);

                if (result.Correct)
                {
                    ViewBag.Titulo = "Correcto";
                    ViewBag.Alert = "success";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "Error";
                    ViewBag.Alert = "danger";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
            }
        }
    }
}
