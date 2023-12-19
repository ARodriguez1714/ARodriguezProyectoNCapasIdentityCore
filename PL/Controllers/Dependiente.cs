using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ML;

namespace PL.Controllers
{
    public class Dependiente : Controller
    {
        public IActionResult GetAll(string numeroEmpleado)
        {
            ML.Dependiente dependiente = new ML.Dependiente();
            if (numeroEmpleado == null)
            {
                ML.Result result = BL.Dependiente.GetAllLINQ();

                if (result.Correct)
                {
                    dependiente.Dependientes = result.Objects;
                    return View(dependiente);
                }
                else
                {
                    ViewBag.Titulo = "Error";
                    ViewBag.Message = "Ocurrio un error";
                    ViewBag.Alert = "danger";
                    return View("Modal");
                }
            }
            else
            {
                ML.Result resultEmpleado = BL.Dependiente.GetByEmpleado(numeroEmpleado);
                if (resultEmpleado.Correct)
                {
                    dependiente.Dependientes = resultEmpleado.Objects;
                    return View(dependiente);
                }
                else
                {
                    ViewBag.Titulo = "Error";
                    ViewBag.Message = "Ocurrio un error";
                    ViewBag.Alert = "danger";
                    return View("Modal");
                }
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Form(int idDependiente, string numeroDependiente)
        {
            ML.Dependiente dependiente = new ML.Dependiente();
            ML.Result resultEmpleados = BL.Empleado.GetAllLINQ();
            dependiente.Empleado = new ML.Empleado();
            dependiente.Empleado.NumeroEmpleado = numeroDependiente;
            if (idDependiente == 0)
            {
                //add
                dependiente.Empleado.Empleados = resultEmpleados.Objects;

                ViewBag.Titulo = "Agregar Usuario";
                ViewBag.Boton = "Agregar";
                return View(dependiente);
            }
            else
            {
                //update
                ML.Result result = BL.Dependiente.GetByIdLINQ(idDependiente);
                if (result.Correct)
                {
                    dependiente = (ML.Dependiente)result.Object;
                    dependiente.Empleado.Empleados = resultEmpleados.Objects;
                    ViewBag.Titulo = "Actualizar Usuario";
                    ViewBag.Boton = "Modificar";
                    return View(dependiente);
                }
                else
                {
                    ViewBag.Titulo = "Error";
                    ViewBag.Alert = "danger";
                    ViewBag.Message = "ERROR AL REALIZAR LA CONSULTA";
                    return View("Modal");
                }
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public IActionResult Form(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();
            if (dependiente.IdDependiente == 0)
            {
                //add
                result = BL.Dependiente.AddLINQ(dependiente);
                if (result.Correct)
                {
                    ViewBag.Titulo = "Exito";
                    ViewBag.Alert = "success";
                    ViewBag.Message = "EL DEPENDIENTE SE ASIGNÓ CON EXITO.";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "Error";
                    ViewBag.Alert = "danger";
                    ViewBag.Message = "DEPENDIENTE NO GREGADO.";
                    return View("Modal");
                }
            }
            else
            {
                //update
                result = BL.Dependiente.Update(dependiente);
                if (result.Correct)
                {
                    ViewBag.Titulo = "Exito";
                    ViewBag.Alert = "success";
                    ViewBag.Message = "EL DEPENDIENTE SE ACTUALIZÓ CON EXITO.";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Titulo = "Error";
                    ViewBag.Alert = "danger";
                    ViewBag.Message = "DEPENDIENTE NO ENCONTRADO.";
                    return View("Modal");
                }
            }
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult Delete(int idDependiente)
        {
            ML.Result result = BL.Dependiente.DeleteLINQ(idDependiente);

            if (result.Correct)
            {
                ViewBag.Titulo = "Exito";
                ViewBag.Alert = "success";
                ViewBag.Message = "EL DEPENDIENTE SE ELIMINO CON EXITO.";
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
