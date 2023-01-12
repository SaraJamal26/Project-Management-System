using Project_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Project_Management_System.Controllers
{
    public class LoginController : Controller
    {
        private ProjectsDbEntities2 db = new ProjectsDbEntities2();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            //ViewBag.manager_id = new SelectList(db.Managers, "id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
      

            using (var context = new ProjectsDbEntities2())
            {
                bool isValid = context.Logins.Any(x => x.username == model.Username
                && x.password == model.Password);

                 if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index","Home");
                }
                ModelState.AddModelError("", "Invalid username or password");
               
                   
                return View();
            }
        }

        public ActionResult LoginManager(Models.Membership model)
        {


            using (var context = new ProjectsDbEntities2())
            {
                bool isValid = context.Logins.Any(x => x.username == model.Username
                && x.password == model.Password);

                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid username or password");


                return View();
            }
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }

    
}