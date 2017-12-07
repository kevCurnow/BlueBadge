using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NetworkingApp.Data;
using NetworkingApp.Models.EventModels;

namespace NetworkingApp.Presentation.Web.Controllers
{
    public class EventsController : Controller
    {
        private NetworkingDBEntities db = new NetworkingDBEntities();

        private EventService CreateEventService()
        {
            var userId = User.Identity.GetUserId();
            var svc = new EventService(userId);

            return svc;
        }

        //GET: Events
        [Authorize]
        public ActionResult Index()
        {
            var model = CreateEventService().GetEvents();
            return View(model);
        }

        public ActionResult Create()
        {
            var model = new EventCreateModel();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventCreateModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if(!CreateEventService().CreateEvent(model))
            {
                ModelState.AddModelError("", "Unable to create event");
                return View(model);
            }

            TempData["SaveResult"] = "Your event was created";

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var model = CreateEventService().GetEventById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var detailModel = CreateEventService().GetEventById(id);

            var editModel =
                new EventEditModel
                {
                    EventID = detailModel.EventID,
                    EventName = detailModel.EventName,
                    Date = detailModel.Date,
                    Location = detailModel.Location
                };

            return View(editModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventEditModel model)
        {
            //Defensive coding
            if (model.EventID != id)
            {
                ModelState.AddModelError("", "Nice try!");
                model.EventID = id;
                return View(model);
            }

            if (!ModelState.IsValid) return View(model);

            if(!CreateEventService().UpdateEvent(model))
            {
                ModelState.AddModelError("", "Unable to update event");
                return View(model);
            }

            TempData["SaveResult"] = "Your event was saved";

            return RedirectToAction("Index");
        }

        //TODO: Figure out if delete functionality is truly needed for event.
        [ActionName("Delete")]
        public ActionResult DeletePost(int id)
        {
            CreateEventService().DeleteEvent(id);

            TempData["SaveResult"] = "Your event was deleted";

            return RedirectToAction("Index");
        }
        //// GET: Events
        //public ActionResult Index()
        //{
        //    var events = db.Events.Include(e => e.User);
        //    return View(events.ToList());
        //}

        //// GET: Events/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Event @event = db.Events.Find(id);
        //    if (@event == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(@event);
        //}

        //// GET: Events/Create
        //public ActionResult Create()
        //{
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName");
        //    return View();
        //}

        //// POST: Events/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "EventID,EventName,Date,Location,UserID")] Event @event)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Events.Add(@event);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", @event.UserID);
        //    return View(@event);
        //}

        //// GET: Events/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Event @event = db.Events.Find(id);
        //    if (@event == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", @event.UserID);
        //    return View(@event);
        //}

        //// POST: Events/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "EventID,EventName,Date,Location,UserID")] Event @event)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(@event).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.UserID = new SelectList(db.Users, "UserID", "UserName", @event.UserID);
        //    return View(@event);
        //}

        //// GET: Events/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Event @event = db.Events.Find(id);
        //    if (@event == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(@event);
        //}

        //// POST: Events/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Event @event = db.Events.Find(id);
        //    db.Events.Remove(@event);
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
