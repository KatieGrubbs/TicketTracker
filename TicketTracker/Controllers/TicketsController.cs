using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TicketTracker.Models;
using TicketTracker.ViewModels;

namespace TicketTracker.Controllers
{
    public class TicketsController : Controller
    {
        // GET: Tickets
        public ActionResult Index()
        {
            var openTickets = new List<TicketViewModel>();

            using (var db = new TicketTrackerContext())
            {
                openTickets = db.Database.SqlQuery<TicketViewModel>("GetOpenTickets").ToList();
            }

            return View(openTickets);
        }

        //// GET: Tickets/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TicketViewModel ticketViewModel = db.TicketViewModels.Find(id);
        //    if (ticketViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ticketViewModel);
        //}

        //// GET: Tickets/Create
        //public ActionResult Create()
        //{
        //    var ticketViewModel = new TicketViewModel();
        //    ticketViewModel.SeverityLevels = 
        //    return View();
        //}

        //// POST: Tickets/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "TicketId,Subject,Description,SeverityLevelId,CategoryId,DateCreated,ReporterId,IsResolved")] TicketViewModel ticketViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.TicketViewModels.Add(ticketViewModel);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(ticketViewModel);
        //}

        //// GET: Tickets/Edit/5
        //[Authorize(Roles = "Admin")]
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TicketViewModel ticketViewModel = db.TicketViewModels.Find(id);
        //    if (ticketViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ticketViewModel);
        //}

        //// POST: Tickets/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Roles = "Admin")]
        //public ActionResult Edit([Bind(Include = "TicketId,Subject,Description,SeverityLevelId,CategoryId,DateCreated,ReporterId,IsResolved")] TicketViewModel ticketViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(ticketViewModel).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(ticketViewModel);
        //}

        //// GET: Tickets/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TicketViewModel ticketViewModel = db.TicketViewModels.Find(id);
        //    if (ticketViewModel == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ticketViewModel);
        //}

        //// POST: Tickets/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    TicketViewModel ticketViewModel = db.TicketViewModels.Find(id);
        //    db.TicketViewModels.Remove(ticketViewModel);
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
