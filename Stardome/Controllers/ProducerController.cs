﻿using Stardome.DomainObjects;
using Stardome.Repositories;
using Stardome.Services.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;

namespace Stardome.Controllers
{
    public class ProducerController : Controller
    {
        //
        // GET: /Producer/

         private IUserAuthCredentialService userAuthCredentialService;
        private IRoleService roleService;
        public ActionResult Index()
        {
            userAuthCredentialService = new UserAuthCredentialService(new UserAuthCredentialRepository(new StardomeEntitiesCS()));
            roleService = new RoleService(new RoleRepository(new StardomeEntitiesCS()));

            if (ModelState.IsValid && WebSecurity.IsAuthenticated)
            {

                int roleId = userAuthCredentialService.GetByUsername(WebSecurity.CurrentUserName).Role.Id;
                if (roleId != 2)
                    return (new AccountController()).RedirectToLocal(roleId);
                else
                {
                    ViewBag.showAdminMenu = false;
                    return View();
                }
            }
            else
            {
                ViewBag.showAdminMenu = false;
                return View();
            }

        }
    }
}