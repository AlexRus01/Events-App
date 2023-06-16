using Events_App.Data;
using Events_App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace Events_App.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext db;

        public CategoriesController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var categories = db.Categories;
            ViewBag.Categories = categories;
            return View();
        }

        public ActionResult Show(int id)
        {
            Category category = db.Categories.Include("Events")
                               .Where(cat => cat.CategoryId == id)
                               .First();
            return View(category);
        }


        [Authorize(Roles = "Editor, Admin")]
        public ActionResult New()
        {
            return View();
        }
        [HttpPost]

        public ActionResult New(Category cat)
        {
            try
            {
                db.Categories.Add(cat);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch (Exception)
            {
                return RedirectToAction("New");
            }

        }

        [Authorize(Roles = "Editor, Admin")]
        public ActionResult Edit(int id)
        {
            Category cat = db.Categories.Find(id);
            ViewBag.Category = cat;
            return View();
        }



        [HttpPost]
        [Authorize(Roles = "Editor, Admin")]

        public ActionResult Edit(int id, Category cat)
        {
            try
            {
                Category cat2 = db.Categories.Find(id);
                {
                    cat2.CategoryName = cat.CategoryName;
                    db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return RedirectToAction("Edit");
            }
        }

        [Authorize(Roles = "Editor, Admin")]

        public ActionResult Delete(int id)
        {
            Category cat = db.Categories.Find(id);
            db.Categories.Remove(cat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
