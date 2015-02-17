<<<<<<< HEAD
﻿using System.Linq;
using System.Web.Mvc;
using Stardome.DomainObjects;
using Stardome.Models;
using Stardome.Repositories;
using Stardome.Services.Domain;

namespace Stardome.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserAuthCredentialService userAuthCredentialService;
        private readonly RoleService roleService;

        public HomeController()
        {
            userAuthCredentialService = new UserAuthCredentialService(new UserAuthCredentialRepository(new StardomeEntitiesCS()));
            roleService = new RoleService(new RoleRepository(new StardomeEntitiesCS()));
        }

        public ActionResult Users()
        {
            var model = new UserManagement
            {
                UserList = userAuthCredentialService.GetUserAuthCredentials().ToList(),
                Roles = roleService.GetRoles().ToList()
            };
            ViewBag.showAdminMenu = true;
            ViewBag.Message = "User Management Page";

            return View(model);
        }

        public ActionResult Content()
        {
            ViewBag.showAdminMenu = true;
            ViewBag.Message = "Content Management Page";

            return View();
        }

        public ActionResult Settings()
        {
            ViewBag.showAdminMenu = true;
            ViewBag.Message = "Settings Page.";

            return View();
        }
    }
}
=======
﻿using System.Linq;
using System.Web.Mvc;
using Stardome.DomainObjects;
using Stardome.Models;
using Stardome.Repositories;
using Stardome.Services.Domain;

namespace Stardome.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserAuthCredentialService userAuthCredentialService;
        private readonly RoleService roleService;

        public HomeController()
        {
            userAuthCredentialService = new UserAuthCredentialService(new UserAuthCredentialRepository(new StardomeEntitiesCS()));
            roleService = new RoleService(new RoleRepository(new StardomeEntitiesCS()));
        }

        public ActionResult Users()
        {
            ViewBag.Message = "User Management Page";
            var model = new UserManagement
            {
                UserList = userAuthCredentialService.GetUserAuthCredentials().ToList(),
                Roles = roleService.GetRoles().ToList()
            };
            ViewBag.showAdminMenu = true;
            

            return View(model);
        }

        public ActionResult Content()
        {
            ViewBag.showAdminMenu = true;
            ViewBag.Message = "Content Management Page";

            return View();
        }

        public ActionResult Settings()
        {
            ViewBag.showAdminMenu = true;
            ViewBag.Message = "Settings Page.";

            return View();
        }
    }
}
>>>>>>> aba16b88a015fe8ab76ee47dc611a91b823828d4
