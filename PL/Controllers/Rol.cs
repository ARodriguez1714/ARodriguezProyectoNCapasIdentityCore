using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ML;
using System.ComponentModel.DataAnnotations;

namespace PL.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class Rol : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        public Rol(RoleManager<IdentityRole> roleMgr)
        {
            roleManager = roleMgr;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var Roles = roleManager.Roles.ToList();
            return View(Roles);
        }

        [HttpGet]
        public IActionResult Form(Guid IdRole, string Name)
        {
            IdentityRole role = new IdentityRole();

            if (Name != null)
            {
                role.Name = Name;
                role.Id = IdRole.ToString();
                return View(role);
            }
            else
            {
                return View(role);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Form([Required] Microsoft.AspNetCore.Identity.IdentityRole rol)
        {

            IdentityRole role = await roleManager.FindByIdAsync(rol.Id.ToString());

            if (role == null)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(rol.Name));
                if (result.Succeeded)
                {
                    return RedirectToAction("GetAll");
                }
            }
            else //Update
            {
                role.Id = await roleManager.GetRoleIdAsync(rol);
                role.Name = await roleManager.GetRoleNameAsync(rol);

                IdentityResult result = await roleManager.UpdateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("GetAll");
                }
            }
            return View();
        }

        public async Task<IActionResult> Delete(Guid IdRole)
        {
            IdentityRole role = await roleManager.FindByIdAsync(IdRole.ToString());

            if (role !=  null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("GetAll");
                }
                else
                {
                    ViewBag.Title = "Error";
                    ViewBag.Message = result.Errors;
                    return View("Modal");
                }
            }
            else
            {
                ViewBag.Title = "Error";
                ViewBag.Message = "No existe ese registro";
                return View("Modal");
            }
        }

        [HttpGet]
        public IActionResult Asignar(Guid IdRole, string Name)
        {
            {
                ML.Result result = BL.UserIdentity.GetAllLINQ();
                ML.UserIdentity user = new ML.UserIdentity();
                if (result.Correct)
                {
                    user.Rol = new ML.Rol();
                    user.IdentityUsers = result.Objects;
                    user.Rol.Name = Name;
                    user.Rol.RoleId = IdRole;
                    return View(user);
                }
                else
                {
                    ViewBag.Title = "Error";
                    ViewBag.Message = "No existe ese registro";
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public IActionResult Asignar(ML.UserIdentity user)
        {

            ML.Result result = BL.UserIdentity.Asignar(user);

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
