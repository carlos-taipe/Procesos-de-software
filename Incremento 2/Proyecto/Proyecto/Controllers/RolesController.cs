using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Proyecto.Models;

namespace Proyecto.Controllers
{
    public class RolesController : Controller
    {
        ApplicationDbContext app = new ApplicationDbContext();
        EdicasEntities1 db = new EdicasEntities1();
        // GET: Roles
        
        public ActionResult Index()
        {
            var data = db.sp_C_Usuarios_Roles().ToList();
            return View(data);
        }

        // POST: Roles/Edit/5
        [HttpPost]
        public ActionResult CambiarRol(string id)
        {
            

                    var roleManager = new RoleManager<IdentityRole>
                        (new RoleStore<IdentityRole>(app));
                    var userManager = new UserManager<ApplicationUser>
                           (new UserStore<ApplicationUser>(app));

                    var UsuarioRol1 = userManager.IsInRole(id, "Administrador");
                    if(UsuarioRol1 == true)
                    {
                        var resultado = userManager.AddToRole(id, "Vendedor");
                        resultado = userManager.RemoveFromRole(id, "Administrador");
                    }
                    else
                    {
                        var resultado = userManager.AddToRole(id, "Administrador");
                        resultado = userManager.RemoveFromRole(id, "Vendedor");
                    } 
                
            return RedirectToAction("Index");

        }

        // GET: Roles/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Roles/Delete/5
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
