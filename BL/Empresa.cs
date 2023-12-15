using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Empresa
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from empresa in context.Empresas
                                 select new
                                 {
                                     IdEmpresa= empresa.IdEmpresa,
                                     NombreEmpresa = empresa.Nombre,
                                     TelefonoEmpresa = empresa.Telefono,
                                     EmailEmpresa = empresa.Telefono,
                                     DireccionWeb = empresa.DireccionWeb
                                 }).ToList();
                    if (query.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (var item in query)
                        {
                            ML.Empresa empresa = new ML.Empresa();

                            empresa.IdEmpresa = item.IdEmpresa;
                            empresa.Nombre = item.NombreEmpresa;
                            empresa.Telefono = item.TelefonoEmpresa;
                            empresa.Email = item.EmailEmpresa;
                            empresa.DireccionWeb = item.DireccionWeb;

                            result.Objects.Add(empresa);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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

        public static ML.Result Add(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    DL.Empresa empresaLINQ = new DL.Empresa();

                    empresaLINQ.Nombre = empresa.Nombre;
                    empresaLINQ.Telefono = empresa.Telefono;
                    empresaLINQ.Email = empresa.Email;
                    empresaLINQ.DireccionWeb = empresa.DireccionWeb;

                    context.Empresas.Add(empresaLINQ);

                    int RowsAffected = context.SaveChanges();
                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "LA EMPRESA SE AGREGO CORRECTAMENTE";
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

        public static ML.Result Update(ML.Empresa empresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from empresaLINQ in context.Empresas
                                 where empresaLINQ.IdEmpresa == empresa.IdEmpresa
                                 select empresaLINQ).FirstOrDefault();

                    if (query != null)
                    {
                        query.Nombre = empresa.Nombre;
                        query.Telefono = empresa.Telefono;
                        query.Email = empresa.Email;
                        query.DireccionWeb = empresa.DireccionWeb;

                        context.SaveChanges();

                        result.Correct = true;
                        result.Message = "LA EMPRESA SE ACTUALIZÓ CORRECTAMENTE CORRECTAMENTE";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Ex = ex;
                result.Message = "ERROR AL REALIZAR LA ACTUALIZACIÓN, " + result.Ex;
                throw;
            }

            return result;
        }

        public static ML.Result GetById(int? idEmpresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    var query = (from empresaLINQ in context.Empresas
                                 where empresaLINQ.IdEmpresa == idEmpresa
                                 select new
                                 {
                                     IdEmpresa = empresaLINQ.IdEmpresa,
                                     NombreEmpresa = empresaLINQ.Nombre,
                                     TelefonoEmpresa = empresaLINQ.Telefono,
                                     Email = empresaLINQ.Email,
                                     DireccionWeb = empresaLINQ.DireccionWeb
                                 }).FirstOrDefault();

                    if (query != null)
                    {
                        ML.Empresa empresa = new ML.Empresa();

                        var item = query;

                        empresa.IdEmpresa = item.IdEmpresa;
                        empresa.Nombre = item.NombreEmpresa;
                        empresa.Telefono = item.TelefonoEmpresa;
                        empresa.Email = item.Email;
                        empresa.DireccionWeb = item.DireccionWeb;

                        result.Object = empresa;

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "NO EXISTE ESE USUARIO.";
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

        public static ML.Result Delete(int? idEmpresa)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.ArodriguezProyectoNcapasIdentityCoreContext context = new DL.ArodriguezProyectoNcapasIdentityCoreContext())
                {
                    DL.Empresa? query = (from empresaLINQ in context.Empresas
                                         where empresaLINQ.IdEmpresa == idEmpresa
                                         select empresaLINQ).FirstOrDefault();

                    context.Empresas.Remove(query);
                    int RowsAffected = context.SaveChanges();

                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                        result.Message = "LA EMPRESA SE ELIMINO CORRECTAMENTE";
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
