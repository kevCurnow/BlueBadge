using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NetworkingApp.Data;
using NetworkingApp.Models.ConnectionModels;
using NetworkingApp.Services;

namespace NetworkingApp.Presentation.Web.Controllers
{
    public class ConnectionsController : Controller
    {
        private NetworkingDBEntities db = new NetworkingDBEntities();

        private ConnectionService CreateConnectionService()
        {
            var userId = User.Identity.GetUserId();
            var svc = new ConnectionService(userId);

            return svc;
        }

        //GET: Connections
        [Authorize]
        public ActionResult Index()
        {
            var model = CreateConnectionService().GetConnections();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new ConnectionCreateModel();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ConnectionCreateModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (!CreateConnectionService().CreateConnection(model))
            {
                ModelState.AddModelError("", "Unable to create connection");
                return View(model);
            }

            TempData["SaveResult"] = "Your connection was created";

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var model = CreateConnectionService().GetConnectionById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var detailModel = CreateConnectionService().GetConnectionById(id);

            var editModel =
                new ConnectionEditModel
                {
                    ConnectionID = detailModel.ConnectionID,
                    ConnectionName = detailModel.ConnectionName,
                    Job = detailModel.Job,
                    Employer = detailModel.Employer, 
                    Phone = detailModel.Phone,
                    Email = detailModel.Email,
                    Notes = detailModel.Notes
                };

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ConnectionEditModel model)
        {
            //Defensive coding
            if (model.ConnectionID != id)
            {
                ModelState.AddModelError("", "Nice try!");
                model.ConnectionID = id;
                return View(model);
            }

            if (!ModelState.IsValid) return View(model);

            if (!CreateConnectionService().UpdateEvent(model))
            {
                ModelState.AddModelError("", "Unable to update connection");
                return View(model);
            }

            TempData["SaveResult"] = "Your connection was saved";

            return RedirectToAction("Index");
        }

        //TODO: Figure out if delete functionality is truly needed for connections.
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            CreateConnectionService().DeleteConnection(id);

            TempData["SaveResult"] = "Your connection was deleted";

            return RedirectToAction("Index");
        }
        //// GET: Connections
        //public ActionResult Index()
        //{
        //    var connections = db.Connections.Include(c => c.Event);
        //    return View(connections.ToList());
        //}

        //// GET: Connections/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Connection connection = db.Connections.Find(id);
        //    if (connection == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(connection);
        //}

        //// GET: Connections/Create
        //public ActionResult Create()
        //{
        //    ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName");
        //    return View();
        //}

        //// POST: Connections/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "ConnectionID,ConnectionName,Job,Employer,Phone,Email,Notes,EventID")] Connection connection)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Connections.Add(connection);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", connection.EventID);
        //    return View(connection);
        //}

        //// GET: Connections/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Connection connection = db.Connections.Find(id);
        //    if (connection == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", connection.EventID);
        //    return View(connection);
        //}

        //// POST: Connections/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "ConnectionID,ConnectionName,Job,Employer,Phone,Email,Notes,EventID")] Connection connection)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(connection).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", connection.EventID);
        //    return View(connection);
        //}

        //// GET: Connections/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Connection connection = db.Connections.Find(id);
        //    if (connection == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(connection);
        //}

        //// POST: Connections/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Connection connection = db.Connections.Find(id);
        //    db.Connections.Remove(connection);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
