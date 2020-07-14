using Proyecto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto.Controllers
{
    public class CategoríasController : Controller
    {
        EdicasEntities1 db = new EdicasEntities1();
        // GET: Categorías
        public ActionResult Index()
        {
            return View();
        }

        // GET: Categorías/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Categorías/Create
        public ActionResult Create(int? idproducto)
        {
            return View();
        }

        // POST: Categorías/Create
        [HttpPost]
        public ActionResult Create(Categorias categoria,int? idproducto)
        {
            try
            {
                db.sp_A_Categoria(categoria.catNombre);
                if(idproducto == null)
                {
                    return RedirectToAction("Create", "Productos");
                }
                else
                {
                    return RedirectToAction("Edit", "Productos", routeValues: new { id = idproducto});
                }  
            }
            catch
            {
                return View();
            }
        }

        // GET: Categorías/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Categorías/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Categorías/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Categorías/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
