using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Proyecto.Controllers
{
    public class HomeController : Controller
    {
        EdicasEntities1 db = new EdicasEntities1();
        public ActionResult Dashboard()
        {
            var estarAutenticado = User.Identity.IsAuthenticated;
            if (estarAutenticado)
            {
                var NombreUsuario = User.Identity.Name;
                var id = User.Identity.GetUserId();
                //using(ApplicationDbContext db = new ApplicationDbContext())
                //{
                //    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
                //    var resultado = roleManager.Create(new IdentityRole("Administrador"));
                //    var Usermanger = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                //    resultado = Usermanger.AddToRole(id, "Administrador");
                //}
            }

            return View();
        }

        public ActionResult Acerca()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contacto()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult Index()
        {
            var data = db.sp_C_Productos().ToList();
            ViewBag.IDCategoria = new SelectList(db.Categorias, "IDCategoria", "catNombre");
            return View(data);
        }

     
        public ActionResult CarritoCompras(int IDProducto, int Cantidad, decimal Precio, string Nombre)
        {
            if (Session["carrito"] == null)
            {
                System.Data.DataTable compras = new DataTable();
                compras.Columns.Add("IDVenta", typeof(Int32));
                compras.Columns.Add("IDProducto", typeof(Int32));
                compras.Columns.Add("Cantidad", typeof(Int32));
                compras.Columns.Add("Nombre", typeof(String));
                compras.Columns.Add("SubTotal", typeof(Decimal));
                compras.Columns.Add("Precio", typeof(Decimal));
                compras.Rows.Add(0, IDProducto, Cantidad, Nombre, Precio * Cantidad, Precio);
                Session["carrito"] = compras;
                return View(compras);
            }
            else
            {
                DataTable compras = (DataTable)Session["carrito"];
                compras.Rows.Add(0, IDProducto, Cantidad, Nombre, Precio * Cantidad, Precio);
                Session["carrito"] = compras;
                return View(compras);
            }
        }

      
        public ActionResult realizarVenta(decimal subTotal, decimal Total)
        {
            DataTable compras = (DataTable)Session["carrito"];
            var data = db.sp_A_Venta(subTotal, Total).First();
            int IDVenta = data.IDVenta;
            int IDProducto;
            int Cantidad;
            decimal SubTotal;
            decimal Precio;
            try
            {
                for (int i = 0; i < compras.Rows.Count; i++)
                {
                    IDProducto = Convert.ToInt32(compras.Rows[i][1]);
                    Cantidad = Convert.ToInt32(compras.Rows[i][2]);
                    SubTotal = Convert.ToDecimal(compras.Rows[i][4]);
                    Precio = Convert.ToDecimal(compras.Rows[i][5]);
                    db.sp_A_DetalleVenta(IDVenta, IDProducto, Cantidad, SubTotal, Precio);
                    db.sp_M_Venta_Stock(IDProducto, Cantidad);
                }
            }
            catch (Exception)
            {
                ViewBag.MessageError = "Error en la venta.";
            }
            

            Session["carrito"] = null;
            var productos = db.sp_C_Productos().ToList();
            ViewBag.IDCategoria = new SelectList(db.Categorias, "IDCategoria", "catNombre");
            return View("Index",productos);
        }

        public ActionResult EliminarProductoCarrito(int id)
        {
            DataTable compras = (DataTable)Session["carrito"];
            compras.Rows.RemoveAt(ObtenerIndex(id));
            return View("CarritoCompras", compras);
        }

        private int ObtenerIndex(int id)
        {
            DataTable compras = (DataTable)Session["carrito"];
            int IDProducto;
            for (int i = 0; i < compras.Rows.Count; i++)
            {
                IDProducto = Convert.ToInt32(compras.Rows[i][1]);
                if (IDProducto == id)
                    return i;
            }
            return 0;
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
    }
}