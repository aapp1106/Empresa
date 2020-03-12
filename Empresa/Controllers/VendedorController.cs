using Empresa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Empresa.Controllers
{
    public class VendedorController : Controller
    {
        // GET: Vendedor
        public ActionResult Index()
        {
            using (var Db = new Empresa.Models.EmpresaConexionString())
            {
                return View(Db.Vendedor.ToList());
            }
        }

        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vendedor V)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var bd = new EmpresaConexionString())
                {
                    Vendedor ExisteVendedor = bd.Vendedor.FirstOrDefault(w => w.Cedula_V == V.Cedula_V);
                    V.Fecha_Registro = DateTime.Now;
                    if (ExisteVendedor == null)
                    {
                        bd.Vendedor.Add(V);
                        bd.SaveChanges();

                    }
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Problemas al crear el Vendedor", e.Message);
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Vendedor V)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                using (var bd = new EmpresaConexionString())
                {
                    Vendedor ExisteVendedor = bd.Vendedor.FirstOrDefault(w => w.Id_Vendedor == V.Id_Vendedor);
                    ExisteVendedor.fecha_modificacion = DateTime.Now;
                    ExisteVendedor.Genero = V.Genero;
                    ExisteVendedor.Nombres_V = V.Nombres_V;
                    ExisteVendedor.Apellidos_V = V.Apellidos_V;
                    ExisteVendedor.Cedula_V = V.Cedula_V;
                    ExisteVendedor.Direccion = V.Direccion;

                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Problemas al editar el producto", e.Message);
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
                    Vendedor ExisteVendedor = bd.Vendedor.FirstOrDefault(w => w.Id_Vendedor == Id);
                    return View(ExisteVendedor);
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Problemas al editar el vendedor", e.Message);
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
                    Vendedor ExisteVendedor = bd.Vendedor.FirstOrDefault(w => w.Id_Vendedor == Id);
                    bd.Vendedor.Remove(ExisteVendedor);
                    bd.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Problemas al eliminar el producto", e.Message);
                return View();
            }



        }
    }
}