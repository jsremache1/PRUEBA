using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Tarea_02.Models;

namespace Tarea_02
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ApplicationDbContext db = new ApplicationDbContext();

            CrearRoles(db);
            CrearSuperUsuario(db);
            CrearADM(db);
            CrearAgente(db);
            AsignarPermisos(db);
            db.Dispose();
        }

        private void CrearRoles (ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            if (!roleManager.RoleExists("Create"))
            {
                roleManager.Create(new IdentityRole { Id = "1", Name = "Create" });
            }
            if (!roleManager.RoleExists("Edit"))
            {
                roleManager.Create(new IdentityRole { Id = "2", Name = "Edit" });
            }
            if (!roleManager.RoleExists("Details"))
            {
                roleManager.Create(new IdentityRole { Id = "3", Name = "Details" });
            }
            if (!roleManager.RoleExists("Delete"))
            {
                roleManager.Create(new IdentityRole { Id = "4", Name = "Delete" });
            }
        }

        private void CrearSuperUsuario(ApplicationDbContext db)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = usermanager.FindByName("SuperAdmin");

            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "SuperAdmin",
                    Email = "superadmin@gmail.com"
                };

                usermanager.Create(user, "superadmin123");
            }
        }

        private void CrearADM(ApplicationDbContext db)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = usermanager.FindByName("ADM");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "ADM",
                    Email = "jsremache1@espe.edu.ec"
                };
                usermanager.Create(user, "adm123");
            }
        }

        private void CrearAgente(ApplicationDbContext db)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = usermanager.FindByName("AGENTE");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "AGENTE",
                    Email = "agente@hotmail.com"
                };
                usermanager.Create(user, "agente123");
            }
        }

        private void AsignarPermisos(ApplicationDbContext db)
        {
            var usermanager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            // SuperAdmin: acceso a todas las opciones del CRUD
            var superAdmin = usermanager.FindByName("SuperAdmin");
            if (superAdmin != null)
            {
                if (!usermanager.IsInRole(superAdmin.Id, "Create"))
                {
                    usermanager.AddToRole(superAdmin.Id, "Create");
                }
                if (!usermanager.IsInRole(superAdmin.Id, "Edit"))
                {
                    usermanager.AddToRole(superAdmin.Id, "Edit");
                }
                if (!usermanager.IsInRole(superAdmin.Id, "Details"))
                {
                    usermanager.AddToRole(superAdmin.Id, "Details");
                }
                if (!usermanager.IsInRole(superAdmin.Id, "Delete"))
                {
                    usermanager.AddToRole(superAdmin.Id, "Delete");
                }
            }

            // ADM: todo menos eliminar
            var adm = usermanager.FindByName("ADM");
            if (adm != null)
            {
                if (!usermanager.IsInRole(adm.Id, "Create"))
                {
                    usermanager.AddToRole(adm.Id, "Create");
                }
                if (!usermanager.IsInRole(adm.Id, "Edit"))
                {
                    usermanager.AddToRole(adm.Id, "Edit");
                }
                if (!usermanager.IsInRole(adm.Id, "Details"))
                {
                    usermanager.AddToRole(adm.Id, "Details");
                }
                // Si ya tenía Delete de una ejecución anterior, se le quita
                if (usermanager.IsInRole(adm.Id, "Delete"))
                {
                    usermanager.RemoveFromRole(adm.Id, "Delete");
                }
            }

            // AGENTE: solo listar y ver el detalle
            var agente = usermanager.FindByName("AGENTE");
            if (agente != null)
            {
                if (!usermanager.IsInRole(agente.Id, "Details"))
                {
                    usermanager.AddToRole(agente.Id, "Details");
                }
            }
        }
    }
}
