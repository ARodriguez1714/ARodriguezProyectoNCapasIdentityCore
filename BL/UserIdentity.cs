using DL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UserIdentity
    {
        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from rol in context.AspNetUsers
                                 select new
                                 {
                                     IdUser = rol.Id,
                                     UserName = rol.UserName
                                 }).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.UserIdentity usuario = new ML.UserIdentity();

                            usuario.IdUsuario = item.IdUser;
                            usuario.UserName = item.UserName;
                            result.Objects.Add(usuario);
                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "ERROR AL REALIZAR LA CONSULTA, " + result.Ex;
                throw;
            }

            return result;
        }

        public static ML.Result Asignar(ML.UserIdentity user)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"AddAspNetUserRoles '{user.IdUsuario}', '{user.Rol.RoleId}'");

                    if (query > 0)
                    {
                        result.Correct = true;
                        result.Message = "EL ROL SE ASIGNO CORRECTAMENTE.";
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "ERROR AL REALIZAR LA CONSULTA.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "ERROR AL REALIZAR LA CONSULTA, " + result.Ex;
                throw;
            }
            
            return result;
        }
    }
}
