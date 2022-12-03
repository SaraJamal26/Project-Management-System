using Project_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
            ViewBag.manager_id = new SelectList(db.Managers, "id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.User model)
        {
            using (var context = new ProjectsDbEntities2())
            {
                bool isValid = context.Users.Any(x => x.Email == model.Email
                && x.Password == model.Password);

                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index", "Projects");
                }
                ModelState.AddModelError("", "Invalid email or password");
                ViewBag.manager_id = new SelectList(db.Managers, "id", "Name");
                return View();
            }
        }

        public ActionResult LoginManager()
        {
            ViewBag.manager_id = new SelectList(db.Managers, "id", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult LoginManager(Models.Manager model)
        {
            using (var context = new ProjectsDbEntities2())
            {
                bool isValid = context.Managers.Any(x => x.Email == model.Email
                && x.Password == model.Password);

                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    return RedirectToAction("Index", "Projects");
                }
                ModelState.AddModelError("", "Invalid email or password");
                ViewBag.manager_id = new SelectList(db.Managers, "id", "Name");
                return View();
            }



        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Projects");
        }
    }

    
}