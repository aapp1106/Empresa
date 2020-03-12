using Empresa.Clases;
using Empresa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Empresa.Controllers
{
    public class NominaController : Controller
    {
        // GET: Nomina
        public ActionResult Index()
        {
            using (var Db = new Empresa.Models.EmpresaConexionString())
            {
                List<Nomina> nom = Db.Nomina.OrderByDescending(o => o.Fecha_Nomina).ToList();
                nom.ForEach (n =>
                {
                    n.Vendedor = Db.Vendedor.FirstOrDefault(a => a.Id_Vendedor == n.Id_Vendedor);
                    n.Rango_venta = Db.Rango_venta.FirstOrDefault(a => a.Id_Rango == n.Id_Rango);
                });

                return View(nom);
            }
        }

        public ActionResult Realizar_Nomina()
        {
            try
            {
                using (var Db = new Empresa.Models.EmpresaConexionString())
                {
                    List<VentasDeVendedoresPorMes2_Result> lis = Db.VentasDeVendedoresPorMes2().ToList();
                    List<Nomina> nom = Db.Nomina.ToList();
                    foreach (var item in lis)
                    {
                        Vendedor ven = Db.Vendedor.FirstOrDefault(v => v.Id_Vendedor == item.Id_Vendedor);

                        if (Db.Nomina.FirstOrDefault(n => n.Fecha_Nomina.Month == item.Mes && n.Fecha_Nomina.Year == item.Anho && n.Id_Vendedor == item.Id_Vendedor ) != null  )
                        {
                            int cantidadDias = (int)(DateTime.Now.Date - ven.Fecha_Registro.Date).TotalDays;
                            if (cantidadDias >= 60)
                            {
                                Nomina nomina = new Nomina()
                                {
                                    Id_Vendedor = item.Id_Vendedor,
                                    Fecha_Nomina = DateTime.Now,
                                    Salario_Basico = (decimal)item.SB,
                                    Auxilio_Transporte = item.AT,
                                    Auxilio_Alimentacion = item.AA,
                                    Id_Rango = (int)item.Id_Rango,
                                    Valor_Sueldo = item.Valor_Sueldo,
                                    SumaVentas = item.SumaVentas
                                };
                                Db.Nomina.Add(nomina);
                                Db.SaveChanges();
                                return RedirectToAction("Index");
                            }
                        }     
                        
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index");
            }
        }

        public void CrearExcel()
        {
            using (var bd=new EmpresaConexionString())
            {

                List<Nomina> ListaLiquidaciones = bd.Nomina.ToList();
                ListaLiquidaciones.ForEach(n =>
                {
                    n.Vendedor = bd.Vendedor.FirstOrDefault(a => a.Id_Vendedor == n.Id_Vendedor);
                    n.Rango_venta = bd.Rango_venta.FirstOrDefault(a => a.Id_Rango == n.Id_Rango);

                });
                try
                {
                    if (System.IO.File.Exists(Server.MapPath("/Content/Excel.xlsx")))
                    {
                        System.IO.File.Delete(Server.MapPath("/Content/Excel.xlsx"));
                    }

                    string ruta = Server.MapPath("/Content/Excel.xlsx");
                    CreateExcelFile.CreateExcelDocument(ListaLiquidaciones.Select(w => new
                    {

                       Fecha = w.Fecha_Nomina.Date.ToShortDateString(),
                       Nombre_Vendedor = w.Vendedor.Nombres_V + " "+w.Vendedor.Apellidos_V,
                       Cedula = w.Vendedor.Cedula_V,
                       Salario_Basico = w.Salario_Basico.ToString("C"),
                       Auxilio_Transporte =w.Auxilio_Transporte,
                       Auxilio_Alimentacion = w.Auxilio_Alimentacion,
                       Porcentaje_Comision = w.Rango_venta.Porcentaje,
                       Sueldo_Total = w.Valor_Sueldo,
                       Total_ventas = w.SumaVentas
                    }
              ).ToList(), ruta);

                    Response.ContentType = "application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"; //set the MIME type here
                    Response.AddHeader("content-disposition", "attachment; filename=Nominas.xlsx");
                    Response.ContentEncoding = System.Text.Encoding.Default;
                    Response.TransmitFile(ruta);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

    }

}