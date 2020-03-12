using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Empresa.Models;
namespace Empresa.Controllers
{
    public class ProductosController : Controller
    {
        // GET: Productos
        public ActionResult Index()
        {
            using (var Db=new Empresa.Models.EmpresaConexionString())
            {
                return View(Db.Productos.ToList());
            }
           
        }
        public ActionResult Create()
        {
            return View();

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Productos P)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var bd = new EmpresaConexionString())
                {
                    Productos ExisteProducto = bd.Productos.FirstOrDefault(w => w.Nombre_Producto == P.Nombre_Producto);
                    if (ExisteProducto == null)
                    {
                        bd.Productos.Add(P);
                        bd.SaveChanges();

                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Problemas al crear el producto", e.Message);
                return View();
            }

            

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Productos P)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var bd = new EmpresaConexionString())
                {
                    Productos ExisteProducto = bd.Productos.FirstOrDefault(w => w.Id_Producto == P.Id_Producto);
                    ExisteProducto.Nombre_Producto = P.Nombre_Producto;
                    ExisteProducto.Valor_Producto = P.Valor_Producto;

                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Problemas al crear el producto", e.Message);
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
                    Productos ExisteProducto = bd.Productos.FirstOrDefault(w => w.Id_Producto == Id);
                    


                    return View(ExisteProducto);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Problemas al crear el producto", e.Message);
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
                    Productos ExisteProducto = bd.Productos.FirstOrDefault(w => w.Id_Producto == Id);
                    bd.Productos.Remove(ExisteProducto);
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Problemas al crear el producto", e.Message);
                return View();
            }



        }
    }
}