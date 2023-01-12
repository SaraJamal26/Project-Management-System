using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_Management_System.Models;

namespace Project_Management_System.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private ProjectsDbEntities2 db = new ProjectsDbEntities2();

        // GET: Tasks
        public ActionResult Index()
        {
            var tasks = db.Tasks.Include(t => t.Project).Include(t => t.User);
            return View(tasks.ToList());
        }

        // GET: Tasks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }
        public ActionResult DetailsbyProject(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var task = db.Tasks.Where(x => x.project_id == id);

            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task.ToList());
        }

        // GET: Tasks/Create
        public ActionResult Create()
        {
            ViewBag.project_id = new SelectList(db.Projects, "id", "project_name");
            ViewBag.user_id = new SelectList(db.Users, "User_id", "Name");
            List<SelectListItem> items = new List<SelectListItem>();

            items.Add(new SelectListItem { Text = "Pending", Value = "Pending", Selected = true });

            items.Add(new SelectListItem { Text = "In Progress", Value = "In Progress" });

            items.Add(new SelectListItem { Text = "Completed", Value = "Completed" });

            ViewBag.status = items;
            
           // ViewBag.status = 
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,title,description,status,createdAt,dueDate,project_id,user_id")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.project_id = new SelectList(db.Projects, "id", "project_name", task.project_id);
            ViewBag.user_id = new SelectList(db.Users, "User_id", "Name", task.user_id);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            ViewBag.project_id = new SelectList(db.Projects, "id", "project_name", task.project_id);
            ViewBag.user_id = new SelectList(db.Users, "User_id", "Name", task.user_id);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,title,description,status,createdAt,dueDate,project_id,user_id")] Task task)
        {
            if (ModelState.IsValid)
            {
                db.Entry(task).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.project_id = new SelectList(db.Projects, "id", "project_name", task.project_id);
            ViewBag.user_id = new SelectList(db.Users, "User_id", "Name", task.user_id);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Task task = db.Tasks.Find(id);
            if (task == null)
            {
                return HttpNotFound();
            }
            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Task task = db.Tasks.Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
