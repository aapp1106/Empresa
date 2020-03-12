using Empresa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Empresa.Controllers
{
    public class VentasController : Controller
    {
        // GET: Ventas
        public ActionResult Index()
        {
            using (var Db = new Empresa.Models.EmpresaConexionString())
            {
                List<Ventas> Venta = Db.Ventas.ToList();

                Venta.ForEach(w => 
                {
                    w.Vendedor = Db.Vendedor.FirstOrDefault(a => a.Id_Vendedor == w.Id_Vendedor);
                    w.Productos=Db.Productos.FirstOrDefault(a => a.Id_Producto == w.Id_Producto);
                });


                return View(Venta);
            }
        }
        public static List<Productos> ListaProductos()
        {
            List<Productos> Productos = new List<Productos>();
            using (var Db = new Empresa.Models.EmpresaConexionString())
            {
                Productos = Db.Productos.ToList();

                
            }
            return Productos;
        }
        public static List<Vendedor> ListaVendedores()
        {
            List<Vendedor> Vendedores = new List<Vendedor>();
            using (var Db = new Empresa.Models.EmpresaConexionString())
            {
                Vendedores = Db.Vendedor.ToList();


            }
            return Vendedores;
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ventas V)
        {
            if (!ModelState.IsValid)
                return View();
            try
            {
                using (var Db = new Empresa.Models.EmpresaConexionString())
                {
                    Productos pro = Db.Productos.FirstOrDefault(p => p.Id_Producto == V.Id_Producto);
                    V.Valor_Producto = pro.Valor_Producto;
                    V.Valor_Total_Ventas = V.Cantidad_Vendida * V.Valor_Producto;

                    
                    V.Fecha_venta = DateTime.Now;

                    Db.Ventas.Add(V);
                    Db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError( "Error.",e.Message);
                return View();
            }
           
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            try
            {
                using (var bd = new EmpresaConexionString())
                {
                    Ventas Existe = bd.Ventas.FirstOrDefault(w => w.Id_Ventas == Id);
                    return View(Existe);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Problemas al buscar la venta", e.Message);
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ventas v)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var bd = new EmpresaConexionString())
                {
                    Productos pro = bd.Productos.FirstOrDefault(p => p.Id_Producto == v.Id_Producto);
                    Ventas Existe = bd.Ventas.FirstOrDefault(w => w.Id_Ventas == v.Id_Ventas);
                    
                    v.Valor_Producto = pro.Valor_Producto;
                    v.Valor_Total_Ventas = v.Cantidad_Vendida * v.Valor_Producto;

                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Problemas al editar la venta", e.Message);
                return View();
            }
        }


        [HttpGet]
        public ActionResult Delete(int Id)
        {
            try
            {
                using (var bd = new EmpresaConexionString())
                {
                    Ventas Existe = bd.Ventas.FirstOrDefault(w => w.Id_Ventas == Id);
                    bd.Ventas.Remove(Existe);
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Problemas al Eliminar la venta", e.Message);
                return View();
            }
        }


    }
}