using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace PL.Controllers
{
    public class Empleado : Controller
    {
        public IActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();
            ML.Result result = BL.Empleado.GetAllLINQ();

            if (result.Correct)
            {

                empleado.Empleados = result.Objects;

                return View(empleado);
            }
            else
            {
                return View(empleado);
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Form(string numeroEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();

            empleado.Empresa = new ML.Empresa();

            //GetAll Empresas

            ML.Result resultEmpresa = BL.Empresa.GetAll();
            empleado.Empresa.Empresas = resultEmpresa.Objects;

            if (numeroEmpleado == null)
            {
                //add
                ViewBag.Titulo = "Agregar Usuario";
                ViewBag.Boton = "Agregar";
                return View(empleado);

            }
            else
            {
                //update
                ML.Result result = BL.Empleado.GetByIdLINQ(numeroEmpleado);

                if (result.Correct)
                {

                    empleado = (ML.Empleado)result.Object;
                    empleado.Empresa.Empresas = resultEmpresa.Objects;
                    ViewBag.Titulo = "Modificar Usuario";
                    ViewBag.Boton = "Modificar";
                    return View(empleado);
                }
                else
                {
                    ViewBag.Alert = "danger";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }

            }
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Form(ML.Empleado empleado, IFormFile fuImagen)
        {
            ML.Result result = new ML.Result();

            if (empleado.NumeroEmpleado == null)
            {
                empleado.Foto = ConvertToBytes(fuImagen);
                result = BL.Empleado.AddLINQ(empleado);

                if (result.Correct)
                {
                    return RedirectToAction("GetAll");
                }
                else
                {
                    ViewBag.Alert = "danger";
                    ViewBag.Message = result.Message;
                    return View("Modal");
                }
            }
            else
            {
                result = BL.Empleado.Update(empleado);

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

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Delete(string NumeroEmpleado)
        {
            ML.Result result = BL.Empleado.DeleteLINQ(NumeroEmpleado);

            if (result.Correct)
            {
                ViewBag.Alert = "success";
                ViewBag.Message = result.Message;
                return View("Modal");
            }
            else
            {
                ViewBag.Alert = "danger";
                ViewBag.Message = result.Message;
                return View("Modal");
            }
        }

        public byte[] ConvertToBytes(IFormFile fuImagen)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                fuImagen.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}
