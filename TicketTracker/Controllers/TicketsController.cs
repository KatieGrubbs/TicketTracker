﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TicketTracker.Models;
using TicketTracker.ViewModels;

namespace TicketTracker.Controllers
{
    public class TicketsController : Controller
    {
        // GET: Tickets/OpenTickets
        public ActionResult OpenTickets()
        {
            using (var db = new TicketTrackerContext())
            {
                // call stored procedure and return a list of open tickets
                var openTickets = db.Database.SqlQuery<TicketViewModel>("GetOpenTickets").ToList();

                return View(openTickets);
            }
        }

        // GET: Tickets/ResolvedTickets
        public ActionResult ResolvedTickets()
        {
            using (var db = new TicketTrackerContext())
            {
                // call stored procedure and return a list of resolved tickets
                var openTickets = db.Database.SqlQuery<TicketViewModel>("GetResolvedTickets").ToList();

                return View(openTickets);
            }
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new TicketTrackerContext())
            {
                // call stored procedure to find ticket by id
                var ticketViewModel = db.Database.SqlQuery<TicketViewModel>("GetTicketById @TicketId",
                    new SqlParameter("TicketId", id)).SingleOrDefault();

                if (ticketViewModel == null)
                {
                    return HttpNotFound();
                }

                return View(ticketViewModel);
            }
        }

        // GET: Tickets/Create
        public ActionResult Create()
        {
            // make sure that dropdownlists in the create form are populated
            var ticketViewModel = new TicketViewModel
            {
                SeverityLevels = GetSeverityLevels(),
                Categories = GetCategories()
            };

            return View(ticketViewModel);
        }

        // POST: Tickets/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TicketViewModel ticketViewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new TicketTrackerContext())
                {
                    // create new ticket from viewmodel property values
                    var ticket = new Ticket
                    {
                        TicketId = ticketViewModel.TicketId,
                        Subject = ticketViewModel.Subject,
                        Description = ticketViewModel.Description,
                        SeverityLevelId = ticketViewModel.SeverityLevelId,
                        CategoryId = ticketViewModel.CategoryId,
                        ReporterId = User.Identity.GetUserId(),     // get logged in user
                        IsResolved = ticketViewModel.IsResolved
                    };

                    db.Tickets.Add(ticket);
                    db.SaveChanges();

                    return RedirectToAction("OpenTickets", "Tickets");
                }
            }

            return View();
        }

        // GET: Tickets/Edit/5
        //[Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new TicketTrackerContext())
            {
                // call stored procedure to find ticket by id
                var ticketViewModel = db.Database.SqlQuery<TicketViewModel>("GetTicketById @TicketId", 
                    new SqlParameter("TicketId", id)).SingleOrDefault();

                if (ticketViewModel == null)
                {
                    return HttpNotFound();
                }

                // make sure that dropdownlists in the edit form are populated
                ticketViewModel.SeverityLevels = GetSeverityLevels();
                ticketViewModel.Categories = GetCategories();

                return View(ticketViewModel);
            }
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TicketViewModel ticketViewModel)
        {
            if (ModelState.IsValid)
            {
                using (var db = new TicketTrackerContext())
                {
                    // create new ticket from viewmodel property values
                    var ticket = new Ticket
                    {
                        TicketId = ticketViewModel.TicketId,
                        Subject = ticketViewModel.Subject,
                        Description = ticketViewModel.Description,
                        SeverityLevelId = ticketViewModel.SeverityLevelId,
                        CategoryId = ticketViewModel.CategoryId,
                        DateCreated = ticketViewModel.DateCreated,
                        ReporterId = ticketViewModel.ReporterId,
                        IsResolved = ticketViewModel.IsResolved
                    };

                    db.Entry(ticket).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("OpenTickets", "Tickets");
                }
            }

            return View();
        }

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

        private IEnumerable<SelectListItem> GetSeverityLevels()
        {
            using (var db = new TicketTrackerContext())
            {
                var severityLevels = db.SeverityLevels.Select(s => new SelectListItem
                {
                    Value = s.SeverityLevelId.ToString(),
                    Text = s.SeverityLevelCode + " - " + s.SeverityLevelName
                }).ToList();

                return new SelectList(severityLevels, "Value", "Text");
            }
        }

        private IEnumerable<SelectListItem> GetCategories()
        {
            using (var db = new TicketTrackerContext())
            {
                var categories = db.Categories.OrderBy(c => c.CategoryName).Select(c => new SelectListItem
                {
                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName
                }).ToList();

                return new SelectList(categories, "Value", "Text");
            }
        }
    }
}