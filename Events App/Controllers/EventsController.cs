using Events_App.Data;
using Events_App.Migrations;
using Events_App.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Xml.Linq;

namespace Events_App.Controllers
{
    public class EventsController : Controller
    {
        private readonly ApplicationDbContext db;

        public EventsController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            var events = db.Events;
            ViewBag.Events = events;
            return View();
        }

        public IActionResult Show(int id)
        {
            Event ev = db.Events.Include("Category")
                                .Include("Comments")
                                .Where(ev => ev.EventId == id)
                                .First();
            return View(ev);
        }

        [HttpPost]
        public IActionResult Show([FromForm] Comment comment)
        {
            comment.Date = DateTime.Now;
            //comment.UserId = _userManager.GetUserId(User);
            if (ModelState.IsValid)
            {
                db.Comments.Add(comment);
                db.SaveChanges();
                return Redirect("/Events/Show/" + comment.EventId);
            }

            else
            {
                Event sub = db.Events.Include("Category")
                                         .Include("Comments")
                                         .Where(sub => sub.EventId == comment.EventId)
                                         .First();

                //return Redirect("/Articles/Show/" + comm.ArticleId);


                return View(sub);
            }
        }

        public IActionResult New()
        {

            Event cls = new Event();

            cls.Categ = GetAllCategories();

            return View(cls);
        }
        [HttpPost]

        public ActionResult New(Event ev)
        {
            try
            {
                db.Events.Add(ev);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return RedirectToAction("New");
            }

        }
        public IActionResult Edit(int id)
        {
            Event ev = db.Events.Include("Category")
                .Where(sub => sub.EventId == id)
                                        .First();
            ev.Categ = GetAllCategories();

          
                return View(ev);
           
        }


 
        [HttpPost]

        public ActionResult Edit(int id, Event ev)
        {
            Event ev2 = db.Events.Find(id);

            if (ModelState.IsValid)
            {

                    ev2.Title = ev.Title;
                    ev2.Description = ev.Description;
                    ev2.CategoryId = ev.CategoryId;
                    ev2.Location = ev.Location;
                    ev2.Date = ev.Date;
                    TempData["message"] = "Evenimentul a fost modificat!";
                    db.SaveChanges();
               
                return RedirectToAction("Index");
            }

            else
            {
                ev.Categ = GetAllCategories();
                return View(ev);
            }
            
                
                
            
            
        }

        public ActionResult Delete(int id)
        {
            Event ev = db.Events.Find(id);
            db.Events.Remove(ev);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [NonAction]
        public IEnumerable<SelectListItem> GetAllCategories()
        {
            // generam o lista de tipul SelectListItem fara elemente
            var selectList = new List<SelectListItem>();

            // extragem toate categoriile din baza de date
            var categories = from cat in db.Categories
                             select cat;

            // iteram prin categorii
            foreach (var category in categories)
            {
                // adaugam in lista elementele necesare pentru dropdown
                // id-ul categoriei si denumirea acesteia
                selectList.Add(new SelectListItem
                {
                    Value = category.CategoryId.ToString(),
                    Text = category.CategoryName.ToString()
                });
            }
            /* Sau se poate implementa astfel: 
             * 
            foreach (var category in categories)
            {
                var listItem = new SelectListItem();
                listItem.Value = category.Id.ToString();
                listItem.Text = category.CategoryName.ToString();

                selectList.Add(listItem);
             }*/


            // returnam lista de categorii
            return selectList;
        }
    }
}
