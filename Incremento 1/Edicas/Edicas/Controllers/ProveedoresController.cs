using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Edicas.Models;

namespace Edicas.Controllers
{
    public class ProveedoresController : Controller
    {
        TienditaEntities db = new TienditaEntities();
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
        public ActionResult Create(Proveedore proveedor)
        {
            try
            {
                db.sp_A_Proveedor(proveedor.RUC, proveedor.provNombre, proveedor.provDireccion, proveedor.provTelefono);
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
            Proveedore proveedor = new Proveedore();
            var data = db.sp_C_ProveedorxRUC(ruc).FirstOrDefault();
            proveedor.RUC = data.RUC;
            proveedor.provNombre = data.provNombre;
            proveedor.provDireccion = data.provDireccion;
            proveedor.provTelefono = data.provTelefono;
            return View(proveedor);
        }

        // POST: Proveedores/Edit/5
        [HttpPost]
        public ActionResult Edit(Proveedore proveedor)
        {
            try
            {
                db.sp_M_Proveedor(proveedor.RUC, proveedor.provNombre, proveedor.provDireccion, proveedor.provTelefono);
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
            var data = db.sp_C_ProveedorxRUC(ruc).FirstOrDefault();
            return View(data);
        }

        // POST: Proveedores/Delete/5
        [HttpPost]
        public ActionResult Delete(string ruc, Proveedore proveedor)
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
