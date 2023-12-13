using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from empleado in context.Empleados
                                 select new
                                 {
                                     NumeroEmpleado = empleado.NumeroEmpleado,
                                     RFC = empleado.Rfc,
                                     Nombre = empleado.Nombre,
                                     ApellidoPaterno = empleado.ApellidoPaterno,
                                     ApellidoMaterno = empleado.ApellidoMaterno,
                                     Email = empleado.Email,
                                     Telefono = empleado.Telefono,
                                     FechaNacimiento = empleado.FechaNacimiento.GetValueOrDefault().ToString("dd/MM/yyyy"),
                                     NSS = empleado.Nss,
                                     FechaIngreso = empleado.FechaIngreso.GetValueOrDefault().ToString("dd/MM/yyyy"),
                                     Foto = empleado.Foto,
                                     IdEmpresa = empleado.IdEmpresa
                                 }).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.NumeroEmpleado = item.NumeroEmpleado;
                            empleado.RFC = item.RFC;
                            empleado.Nombre = item.Nombre;
                            empleado.ApellidoPaterno = item.ApellidoPaterno;
                            empleado.ApellidoMaterno = item.ApellidoMaterno;
                            empleado.Email = item.Email;
                            empleado.Telefono = item.Telefono;
                            empleado.FechaNacimiento = item.FechaNacimiento;
                            empleado.NSS = item.NSS;
                            empleado.FechaIngreso = item.FechaIngreso;
                            empleado.Foto = item.Foto;
                            empleado.IdEmpresa = item.IdEmpresa;
                            result.Objects.Add(empleado);
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

        public static ML.Result AddLINQ(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    DL.Empleado empleadoLINQ = new DL.Empleado();

                    string? queryNumeroEmpleado = $"{char.ToUpper(empleado.Nombre[0])}{char.ToUpper(empleado.ApellidoPaterno[0])}{char.ToUpper(empleado.ApellidoMaterno[0])}" +
                        $"{empleado.FechaNacimiento.Replace("/", "")}" +
                        $"{DateTime.Now.ToString("ddMMyyyyHHmmss").Replace("/", "").Replace(" ", "").Replace(":", "")}";

                    string? queryFechaIngreso = $"{DateTime.Now}";

                    //empleadoLINQ.NumeroEmpleado = empleado.NumeroEmpleado;
                    empleadoLINQ.NumeroEmpleado = queryNumeroEmpleado;
                    empleadoLINQ.Rfc = empleado.RFC;
                    empleadoLINQ.Nombre = empleado.Nombre;
                    empleadoLINQ.ApellidoPaterno = empleado.ApellidoPaterno;
                    empleadoLINQ.ApellidoMaterno = empleado.ApellidoMaterno;
                    empleadoLINQ.Email = empleado.Email;
                    empleadoLINQ.Telefono = empleado.Telefono;
                    empleadoLINQ.FechaNacimiento = DateTime.ParseExact(empleado.FechaNacimiento, "dd/MM/yyyy", null);
                    empleadoLINQ.Nss = empleado.NSS;
                    empleadoLINQ.FechaIngreso = DateTime.Parse(queryFechaIngreso);
                    empleadoLINQ.Foto = empleado.Foto;
                    empleadoLINQ.IdEmpresa = empleado.IdEmpresa;

                    context.Empleados.Add(empleadoLINQ);

                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "SE AGREGO CORRECTAMENTE AL EMPLEADO";
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
        public static ML.Result DeleteLINQ(string numeroEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from empleadoLINQ in context.Empleados
                                 where empleadoLINQ.NumeroEmpleado == numeroEmpleado
                                 select empleadoLINQ).FirstOrDefault();

                    context.Empleados.Remove(query);
                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "EL EMPLEADO SE ELIMINO CORRECTAMENTE";
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
        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from empleadoLINQ in context.Empleados
                                 where empleadoLINQ.NumeroEmpleado == empleado.NumeroEmpleado
                                 select empleadoLINQ).FirstOrDefault();

                    if (query != null)
                    {
                        query.Rfc = empleado.RFC;
                        query.Nombre = empleado.Nombre;
                        query.ApellidoPaterno = empleado.ApellidoPaterno;
                        query.ApellidoMaterno = empleado.ApellidoMaterno;
                        query.Email = empleado.Email;
                        query.Telefono = empleado.Telefono;
                        query.FechaNacimiento = DateTime.Parse(empleado.FechaNacimiento);
                        query.Nss = empleado.NSS;
                        query.FechaIngreso = DateTime.Parse(empleado.FechaIngreso);
                        query.Foto = empleado.Foto;
                        query.IdEmpresa = empleado.IdEmpresa;

                        context.SaveChanges();

                        result.Correct = true;
                        result.Message = "SE ACTUALIZÓ CORRECTAMENTE EL EMPLEADO.";
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
        public static ML.Result GetByIdLINQ(string numeroEmpleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from empleadoLINQ in context.Empleados
                                 where empleadoLINQ.NumeroEmpleado == numeroEmpleado
                                 select new
                                 {
                                     NumeroEmpleado = empleadoLINQ.NumeroEmpleado,
                                     RFC = empleadoLINQ.Rfc,
                                     Nombre = empleadoLINQ.Nombre,
                                     ApellidoPaterno = empleadoLINQ.ApellidoPaterno,
                                     ApellidoMaterno = empleadoLINQ.ApellidoMaterno,
                                     Email = empleadoLINQ.Email,
                                     Telefono = empleadoLINQ.Telefono,
                                     FechaNacimiento = empleadoLINQ.FechaNacimiento.GetValueOrDefault().ToString("dd/MM/yyyy"),
                                     NSS = empleadoLINQ.Nss,
                                     FechaIngreso = empleadoLINQ.FechaIngreso.GetValueOrDefault().ToString("dd/MM/yyyy"),
                                     Foto = empleadoLINQ.Foto,
                                     IdEmpresa = empleadoLINQ.IdEmpresa
                                 });
                    if (query != null)
                    {
                        ML.Empleado empleado = new ML.Empleado();

                        var item = query.FirstOrDefault();

                        empleado.NumeroEmpleado = item.NumeroEmpleado;
                        empleado.RFC = item.RFC;
                        empleado.Nombre = item.Nombre;
                        empleado.ApellidoPaterno = item.ApellidoPaterno;
                        empleado.ApellidoMaterno = item.ApellidoMaterno;
                        empleado.Email = item.Email;
                        empleado.Telefono = item.Telefono;
                        empleado.FechaNacimiento = item.FechaNacimiento;
                        empleado.NSS = item.NSS;
                        empleado.FechaIngreso = item.FechaIngreso;
                        empleado.Foto = item.Foto;
                        empleado.IdEmpresa = item.IdEmpresa;

                        result.Object = empleado;

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

    }
}
