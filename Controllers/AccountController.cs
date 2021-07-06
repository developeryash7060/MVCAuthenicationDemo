using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AuthenicationMvcDemo.Models;
using System.Web.Security;

namespace AuthenicationMvcDemo.Controllers
{ 
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            using (var context = new OfficeEntities())
            {
                bool isValid = context.Users.Any(x => x.UserName == model.UserName && x.Password == model.Password);
                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName,true);
                    return RedirectToAction("index", "Employees");
                }
                else
                {
                    ModelState.AddModelError("","Invalid username or password");
                    return View();
                }
            }
            
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User model)
        {
            using(var context = new OfficeEntities())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");

        }
    }
}