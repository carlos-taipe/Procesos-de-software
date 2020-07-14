using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class ProveedoresController : Controller
    {
        EdicasEntities1 db = new EdicasEntities1();
        // GET: Proveedores
        public ActionResult Index()
        {
            var data = db.sp_C_Proveedores().ToList();
            return View(data);
        }

        // GET: Proveedores/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Proveedores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedores/Create
        [HttpPost]
        public ActionResult Create(Proveedores proveedor)
        {
            try
            {
                db.sp_A_Proveedor(proveedor.RUC, proveedor.provNombres, proveedor.provDireccion, proveedor.provTelefono);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Proveedores/Edit/5
        public ActionResult Edit(string ruc)
        {
            Proveedores proveedor = new Proveedores();
            var data = db.sp_C_ProveedorxRUC(ruc).First();
            proveedor.RUC = data.RUC;
            proveedor.provNombres = data.provNombres;
            proveedor.provDireccion = data.provDireccion;
            proveedor.provTelefono = data.provTelefono;
            return View(proveedor);
        }

        // POST: Proveedores/Edit/5
        [HttpPost]
        public ActionResult Edit(Proveedores proveedor)
        {
            try
            {
                db.sp_M_Proveedor(proveedor.RUC, proveedor.provNombres, proveedor.provDireccion, proveedor.provTelefono);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Proveedores/Delete/5
        public ActionResult Delete(string ruc)
        {
            var data = db.sp_C_ProveedorxRUC(ruc).First();
            return View(data);
        }

        // POST: Proveedores/Delete/5
        [HttpPost]
        public ActionResult Delete(string ruc, FormCollection collection)
        {
            try
            {
                db.sp_E_Proveedor(ruc);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
