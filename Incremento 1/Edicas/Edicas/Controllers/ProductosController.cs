using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Edicas.Models;


namespace Edicas.Controllers
{
    public class ProductosController : Controller
    {
        TienditaEntities db = new TienditaEntities();
        // GET: Productos
        public ActionResult Index()
        {
            var data = db.sp_C_Productos().ToList();
            return View(data);
        }

        // GET: Productos/Create
        [Authorize(Roles ="Administrador")]
        public ActionResult Create()
        {
            ViewBag.RUC = new SelectList(db.Proveedores, "RUC", "provNombre");
            ViewBag.IDCategoria = new SelectList(db.Categorias, "IDCategoria", "catNombre");
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        public ActionResult Create(Producto producto, HttpPostedFileBase Imagen)
        {
            try
            {
                producto.proImagen = new byte[Imagen.ContentLength];
                Imagen.InputStream.Read(producto.proImagen, 0, Imagen.ContentLength);
                db.sp_A_Producto(producto.proNombre, producto.proCantidad, producto.proPrecio, producto.proImagen, producto.IDCategoria,producto.RUC);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Productos/Edit/5
        public ActionResult Edit(int id)
        {
            Producto producto = new Producto();
            var data = db.sp_C_ProductoxID(id).FirstOrDefault();
            producto.IDProducto = data.IDProducto;
            producto.proNombre = data.proNombre;
            producto.proCantidad = data.proCantidad;
            producto.proPrecio = data.proPrecio;
            producto.IDCategoria = data.IDCategoria;
            producto.RUC = data.RUC;
            ViewBag.IDCategoria = new SelectList(db.Categorias, "IDCategoria", "catNombre",producto.IDCategoria);
            ViewBag.RUC = new SelectList(db.Proveedores, "RUC", "provNombre", producto.RUC);
            return View(producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        public ActionResult Edit(Producto producto,HttpPostedFileBase Imagen)
        {
            byte[] ImagenActual = null;
            
            if (Imagen == null)
            {
                ImagenActual = db.Productos.SingleOrDefault(t => t.IDProducto == producto.IDProducto).proImagen;
                db.sp_M_Producto(producto.IDProducto, producto.proNombre, producto.proCantidad, producto.proPrecio, ImagenActual, producto.IDCategoria,producto.RUC);
                return RedirectToAction("Index");
            }
            else
            {
                producto.proImagen = new byte[Imagen.ContentLength];
                Imagen.InputStream.Read(producto.proImagen, 0, Imagen.ContentLength);
                db.sp_M_Producto(producto.IDProducto, producto.proNombre, producto.proCantidad, producto.proPrecio, producto.proImagen, producto.IDCategoria,producto.RUC);
                return RedirectToAction("Index");
            }
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.sp_C_ProductoxID(id).FirstOrDefault();
            return View(data);
        }

        // POST: Productos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id,FormCollection form)
        {
            try
            {
                db.sp_E_Producto(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult obtenerImagen(int id)
        {
            Producto producto = db.Productos.Find(id);
            byte[] byteImagen = producto.proImagen;
            MemoryStream memoria = new MemoryStream(byteImagen);
            Image imagen = Image.FromStream(memoria);
            memoria = new MemoryStream();
            imagen.Save(memoria,ImageFormat.Jpeg);
            memoria.Position = 0;
            return File(memoria,"image/jpg");
        }
    }
}
