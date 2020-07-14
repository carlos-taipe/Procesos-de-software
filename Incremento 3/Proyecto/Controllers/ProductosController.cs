using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Proyecto.Models;

namespace Proyecto.Controllers
{ 
    [Authorize (Roles ="Administrador")]
    public class ProductosController : Controller
    {
        EdicasEntities1 db = new EdicasEntities1();
        // GET: Productos
        public ActionResult Index()
        {
            var data = db.sp_C_Productos().ToList();
            ViewBag.IDCategoria = new SelectList(db.Categorias, "IDCategoria", "catNombre");
            return View(data);
        }

        // GET: Productos/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Productos/Create
        public ActionResult Create()
        {
            ViewBag.IDCategoria = new SelectList(db.Categorias, "IDCategoria", "catNombre");
            ViewBag.RUC = new SelectList(db.Proveedores, "RUC", "provNombres");
            return View();
        }

        // POST: Productos/Create
        [HttpPost]
        public ActionResult Create(Productos producto, HttpPostedFileBase Imagen)
        {
            try
            {
                producto.proImagen = new byte[Imagen.ContentLength];
                Imagen.InputStream.Read(producto.proImagen, 0, Imagen.ContentLength);
                db.sp_A_Producto(producto.proNombre, producto.proCantidad, producto.proPrecioVenta, producto.IDCategoria, producto.RUC,producto.proImagen);
                ViewBag.IDCategoria = new SelectList(db.Categorias, "IDCategoria", "catNombre");
                ViewBag.RUC = new SelectList(db.Proveedores, "RUC", "provNombres");
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
            Productos producto = new Productos();
            var data = db.sp_C_ProductoxID(id).FirstOrDefault();
            producto.IDProducto = data.IDProducto;
            producto.proNombre = data.proNombre;
            producto.proCantidad = data.proCantidad;
            producto.proPrecioVenta = data.proPrecioVenta;
            producto.IDCategoria = data.IDCategoria;
            producto.RUC = data.RUC;
            ViewBag.IDCategoria = new SelectList(db.Categorias, "IDCategoria", "catNombre", producto.IDCategoria);
            ViewBag.RUC = new SelectList(db.Proveedores, "RUC", "provNombres", producto.RUC);
            return View(producto);
        }

        // POST: Productos/Edit/5
        [HttpPost]
        public ActionResult Edit(Productos producto, HttpPostedFileBase Imagen)
        {
            byte[] ImagenActual = null;

            if (Imagen == null)
            {
                ImagenActual = db.Productos.SingleOrDefault(t => t.IDProducto == producto.IDProducto).proImagen;
                db.sp_M_Producto(producto.IDProducto, producto.proNombre, producto.proPrecioVenta, producto.IDCategoria, producto.RUC,ImagenActual);
                return RedirectToAction("Index");
            }
            else
            {
                producto.proImagen = new byte[Imagen.ContentLength];
                Imagen.InputStream.Read(producto.proImagen, 0, Imagen.ContentLength);
                db.sp_M_Producto(producto.IDProducto, producto.proNombre, producto.proPrecioVenta, producto.IDCategoria, producto.RUC,producto.proImagen);
                return RedirectToAction("Index");
            }
        }

        // GET: Productos/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.sp_C_ProductoxID(id).First();
            return View(data);
        }

        // POST: Productos/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
            Productos producto = db.Productos.Find(id);
            byte[] byteImagen = producto.proImagen;
            MemoryStream memoria = new MemoryStream(byteImagen);
            Image imagen = Image.FromStream(memoria);
            memoria = new MemoryStream();
            imagen.Save(memoria, ImageFormat.Jpeg);
            memoria.Position = 0;
            return File(memoria, "image/jpg");
        }

        [HttpPost]
        public ActionResult agregarCantidad(int id,int cantidad)
        {
            db.sp_M_Producto_Cantidad(id, cantidad);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult buscarxCategoria(int IDCategoria)
        {
            ViewBag.IDCategoria = new SelectList(db.Categorias, "IDCategoria", "catNombre");
            var data = db.sp_C_ProductoxCategoria(IDCategoria).ToList();
            return View(data);
        }

        [HttpPost]
        public ActionResult buscarxNombre(string Nombre)
        {
            var data = db.sp_C_ProductoxNombre(Nombre).ToList();
            return View(data);
        }

        [HttpPost]
        public ActionResult buscarProductosAgotados()
        {
            var data = db.sp_C_Productos_Agotados().ToList();
            return View(data);
        }

       
    }
}
