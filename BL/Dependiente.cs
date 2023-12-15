using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Dependiente
    {
        public static ML.Result GetAllLINQ()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from dependiente in context.Dependientes
                                 select new
                                 {
                                     IdDependiente = dependiente.IdDependiente,
                                     NumeroEmpleado = dependiente.NumeroEmpleado,
                                     Nombre = dependiente.Nombre,
                                     ApellidoPaterno = dependiente.ApellidoPaterno,
                                     ApellidoMaterno = dependiente.ApellidoMaterno,
                                     FechaNacimiento = dependiente.FechaNacimiento.GetValueOrDefault().ToString("dd/MM/yyyy"),
                                     EstadoCivil = dependiente.EstadoCivil,
                                     Genero = dependiente.Genero,
                                     Telefono = dependiente.Telefono,
                                     RFC = dependiente.Rfc
                                 }).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Dependiente dependiente = new ML.Dependiente();
                            dependiente.IdDependiente = item.IdDependiente;
                            dependiente.Empleado = new ML.Empleado();
                            dependiente.Empleado.NumeroEmpleado = item.NumeroEmpleado;
                            dependiente.Nombre = item.Nombre;
                            dependiente.ApellidoPaterno = item.ApellidoPaterno;
                            dependiente.ApellidoMaterno = item.ApellidoMaterno;
                            dependiente.FechaNacimiento = item.FechaNacimiento;
                            dependiente.EstadoCivil = item.EstadoCivil;
                            dependiente.Genero = item.Genero;
                            dependiente.Telefono = item.Telefono;
                            dependiente.RFC = item.RFC;
                            result.Objects.Add(dependiente);
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
        public static ML.Result AddLINQ(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    DL.Dependiente dependienteLINQ = new DL.Dependiente();


                    //dependienteLINQ.NumeroEmpleado = empleado.NumeroEmpleado;
                    dependienteLINQ.NumeroEmpleado = dependiente.Empleado.NumeroEmpleado;
                    dependienteLINQ.Nombre = dependiente.Nombre;
                    dependienteLINQ.ApellidoPaterno = dependiente.ApellidoPaterno;
                    dependienteLINQ.ApellidoMaterno = dependiente.ApellidoMaterno;
                    dependienteLINQ.FechaNacimiento = DateTime.Parse(dependiente.FechaNacimiento);
                    dependienteLINQ.EstadoCivil = dependiente.EstadoCivil;
                    dependienteLINQ.Genero = dependiente.Genero;
                    dependienteLINQ.Telefono = dependiente.Telefono;
                    dependienteLINQ.Rfc = dependiente.RFC;

                    context.Dependientes.Add(dependienteLINQ);

                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "SE AGREGO CORRECTAMENTE EL DEPENDIENTE";
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
        public static ML.Result DeleteLINQ(int idDependiente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from dependienteLINQ in context.Dependientes
                                 where dependienteLINQ.IdDependiente == idDependiente
                                 select dependienteLINQ).FirstOrDefault();

                    context.Dependientes.Remove(query);
                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "EL DEPENDIETE SE ELIMINO CORRECTAMENTE";
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
        public static ML.Result Update(ML.Dependiente dependiente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from dependienteLINQ in context.Dependientes
                                 where dependienteLINQ.IdDependiente == dependiente.IdDependiente
                                 select dependienteLINQ).FirstOrDefault();

                    if (query != null)
                    {
                        query.NumeroEmpleado = dependiente.Empleado.NumeroEmpleado;
                        query.Nombre = dependiente.Nombre;
                        query.ApellidoPaterno = dependiente.ApellidoPaterno;
                        query.ApellidoMaterno = dependiente.ApellidoMaterno;
                        query.FechaNacimiento = DateTime.Parse(dependiente.FechaNacimiento);
                        query.EstadoCivil = dependiente.EstadoCivil;
                        query.Genero = dependiente.Genero;
                        query.Telefono = dependiente.Telefono;
                        query.Rfc = dependiente.RFC;

                        context.SaveChanges();

                        result.Correct = true;
                        result.Message = "SE ACTUALIZÓ CORRECTAMENTE EL DEPENDIENTE.";
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
        public static ML.Result GetByIdLINQ(int idDependiente)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from dependienteLINQ in context.Dependientes
                                 where dependienteLINQ.IdDependiente == idDependiente
                                 select new
                                 {
                                     IdDependiente = dependienteLINQ.IdDependiente,
                                     NumeroEmpleado = dependienteLINQ.NumeroEmpleado,
                                     Nombre = dependienteLINQ.Nombre,
                                     ApellidoPaterno = dependienteLINQ.ApellidoPaterno,
                                     ApellidoMaterno = dependienteLINQ.ApellidoMaterno,
                                     FechaNacimiento = dependienteLINQ.FechaNacimiento.GetValueOrDefault().ToString("dd/MM/yyyy"),
                                     EstadoCivil = dependienteLINQ.EstadoCivil,
                                     Genero = dependienteLINQ.Genero,
                                     Telefono = dependienteLINQ.Telefono,
                                     RFC = dependienteLINQ.Rfc
                                 });
                    if (query != null)
                    {
                        ML.Dependiente dependiente = new ML.Dependiente();

                        var item = query.FirstOrDefault();

                        dependiente.IdDependiente = item.IdDependiente;
                        dependiente.Empleado = new ML.Empleado();
                        dependiente.Empleado.NumeroEmpleado = item.NumeroEmpleado;
                        dependiente.Nombre = item.Nombre;
                        dependiente.ApellidoPaterno = item.ApellidoPaterno;
                        dependiente.ApellidoMaterno = item.ApellidoMaterno;
                        dependiente.FechaNacimiento = item.FechaNacimiento;
                        dependiente.EstadoCivil = item.EstadoCivil;
                        dependiente.Genero = item.Genero;
                        dependiente.Telefono = item.Telefono;
                        dependiente.RFC = item.RFC;

                        result.Object = dependiente;

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
