using IdentitySample.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FlatCompStudio.Controllers
{
    public class AccountController : Controller
    {
        public ApplicationUserManager usermngr
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }

        }

        // GET: Account
        public void InitializeRoles()
        {
            var adminrole = "admin";
            var rolmngr = HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            var role = rolmngr.FindByName(adminrole);
            if (role == null)
            {
                rolmngr.Create(new IdentityRole(adminrole));
            }
            string adminusername = "admin@admin.com";
            var usermngr = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            if (usermngr.FindByName(adminusername) == null)
            {
                ApplicationUser adminuser = new ApplicationUser
                {
                    Email = adminusername,
                    UserName = adminusername,
                    Name = "Admin",
                    //img = System.IO.File.ReadAllBytes(Server.MapPath("~/images/team/admin.png"))
                };
                if (usermngr.Create(adminuser, "123456789Oo*").Succeeded)
                {
                    var token = usermngr.GenerateEmailConfirmationToken(adminuser.Id);
                    usermngr.ConfirmEmail(adminuser.Id, token);
                    usermngr.AddToRole(adminuser.Id, "Admin");
                }

            }
        }

        public ActionResult Login()
        {
            InitializeRoles();

            return View();
        }

        public ActionResult LoginConfirm(LoginViewModel model)
        {
            if (ModelState.IsValid == false)
            {
                //TempData["popmessage"] =
                //new PopupMessage { title = "Error", message = "username or password not true" };
                return RedirectToAction("Login", "Account");
            }
            var signinmngr = HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            ApplicationUser user = usermngr.FindByName(model.Email);
            if (user == null)
            {
                //TempData["popmessage"] =
                //new PopupMessage { title = "Error", message = "username or password not true" };
                return RedirectToAction("Login", "Account");
            }

            if (usermngr.IsEmailConfirmed(user.Id))
            {
                var result = signinmngr
                                .PasswordSignIn(model.Email, model.Password, model.RememberMe, false);

                if (result == SignInStatus.Success)
                {
                    //TempData["popmessage"] =
                    //    new PopupMessage { title = "Login", message = "your log in" };
                    if (model.ReturnUrl != null)
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {


                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    //TempData["popmessage"] =
                    //    new PopupMessage { title = "Error", message = "password or username is incer" };
                    return RedirectToAction("Login", "Account");

                }
            }
            else
            {
                //TempData["popmessage"] =
                //        new PopupMessage { title = "Error", message = "please confirmed your mail first then login again" };
            }
            return RedirectToAction("Login", "Account");


        }

        public ActionResult Index()
        {
            return View();
        }
    }
}