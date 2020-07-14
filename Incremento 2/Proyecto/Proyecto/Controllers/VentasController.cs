using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using Proyecto.Models;
using System.ComponentModel;

namespace Proyecto.Controllers
{
    public class VentasController : Controller
    {
        EdicasEntities1 db = new EdicasEntities1();
        // GET: Ventas
        public ActionResult Index()
        {
            DataTable dt = new DataTable();
            return View(dt);
        }

        //Metodo para convertir una lista en tabla
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

        //Metodo para buscar un producto por su nombre.
        public ActionResult buscarxNombre(string Nombre)
        {
            var productos = db.sp_C_ProductoxNombre(Nombre).ToList();
            DataTable dt = ConvertToDataTable(productos);
            return View("Index",dt);
        }

        //Carrito de compras
        [HttpGet]
        public ActionResult CarritoCompras(int IDProducto,int Cantidad,decimal Precio,string Nombre)
        {
            if (Session["carrito"] == null)
            {
                DataTable compras = new DataTable();
                compras.Columns.Add("IDVenta", typeof(Int32));
                compras.Columns.Add("IDProducto", typeof(Int32));
                compras.Columns.Add("Cantidad", typeof(Int32));
                compras.Columns.Add("Nombre", typeof(String));
                compras.Columns.Add("SubTotal", typeof(Decimal));
                compras.Columns.Add("Precio", typeof(Decimal));
                compras.Rows.Add(0,IDProducto,Cantidad,Nombre,Precio*Cantidad,Precio);
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

        //Eliminar producto del carrito
       public ActionResult EliminarProductoCarrito(int id)
        {
            DataTable compras = (DataTable)Session["carrito"];
            compras.Rows.RemoveAt(ObtenerIndex(id));
            return View("CarritoCompras",compras);
        }


        //Obtener el numero de indice del producto para eliminarlo del carrito
        private int ObtenerIndex(int id)
        {
            DataTable compras = (DataTable)Session["carrito"];
            int IDProducto;
            for (int i = 0; i < compras.Rows.Count; i++)
            {
                IDProducto = Convert.ToInt32(compras.Rows[i][1]);
                if ( IDProducto == id)
                    return i;
            }
            return 0;
        }

        //Realizar la venta
        public ActionResult realizarVenta(decimal subTotal,decimal Total)
        {
            DataTable dt = new DataTable();
            DataTable compras = (DataTable)Session["carrito"];
            var data = db.sp_A_Venta(subTotal,Total).First();
            int IDVenta = data.IDVenta;
            int IDProducto;
            int Cantidad;
            decimal SubTotal;
            decimal Precio;
            for (int i = 0; i < compras.Rows.Count; i++)
            {
                IDProducto = Convert.ToInt32(compras.Rows[i][1]);
                Cantidad = Convert.ToInt32(compras.Rows[i][2]);
                SubTotal = Convert.ToDecimal(compras.Rows[i][4]);
                Precio = Convert.ToDecimal(compras.Rows[i][5]);
                db.sp_A_DetalleVenta(IDVenta, IDProducto, Cantidad, SubTotal,Precio);
            }

            Session["carrito"] = null;
            return View("Index",dt);
        }
    }
}
