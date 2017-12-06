using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NetworkingApp.Data;

namespace NetworkingApp.Presentation.Web.Controllers
{
    public class ConnectionsController : Controller
    {
        private NetworkingDBEntities db = new NetworkingDBEntities();

        // GET: Connections
        public ActionResult Index()
        {
            var connections = db.Connections.Include(c => c.Event);
            return View(connections.ToList());
        }

        // GET: Connections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Connection connection = db.Connections.Find(id);
            if (connection == null)
            {
                return HttpNotFound();
            }
            return View(connection);
        }

        // GET: Connections/Create
        public ActionResult Create()
        {
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName");
            return View();
        }

        // POST: Connections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConnectionID,ConnectionName,Job,Employer,Phone,Email,Notes,EventID")] Connection connection)
        {
            if (ModelState.IsValid)
            {
                db.Connections.Add(connection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", connection.EventID);
            return View(connection);
        }

        // GET: Connections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Connection connection = db.Connections.Find(id);
            if (connection == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", connection.EventID);
            return View(connection);
        }

        // POST: Connections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConnectionID,ConnectionName,Job,Employer,Phone,Email,Notes,EventID")] Connection connection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(connection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", connection.EventID);
            return View(connection);
        }

        // GET: Connections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Connection connection = db.Connections.Find(id);
            if (connection == null)
            {
                return HttpNotFound();
            }
            return View(connection);
        }

        // POST: Connections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Connection connection = db.Connections.Find(id);
            db.Connections.Remove(connection);
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
