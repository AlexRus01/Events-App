using Events_App.Models;
using Events_App.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Events_App.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext db;

        //private readonly UserManager<ApplicationUser> _userManager;

        //private readonly RoleManager<IdentityRole> _roleManager;
        public CommentsController(
            ApplicationDbContext context
            //UserManager<ApplicationUser> userManager,
           // RoleManager<IdentityRole> roleManager)
           )
        {
            db = context;

            //_userManager = userManager;

            //_roleManager = roleManager;
        }
        // Stergerea unui comentariu asociat unui articol din baza de date
        [HttpPost]
        //[Authorize(Roles = "User,Editor,Admin")]

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit(int id)
        {
            Comment comm = db.Comments.Find(id);

           // if (comm.UserId == _userManager.GetUserId(User) || User.IsInRole("Editor"))
            //{
                return View(comm);
            //}

            //else
            //{
            //    TempData["message"] = "Nu aveti dreptul sa editati comentariul";
            //    return RedirectToAction("Index", "Subjects");
            //}
        }
        [HttpPost]
        //[Authorize(Roles = "User,Editor,Admin")]

        public IActionResult Edit(int id, Comment requestComment)
        {
            Comment comm = db.Comments.Find(id);

            //if (comm.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin"))
            //{
                if (ModelState.IsValid)
                {
                    comm.Content = requestComment.Content;

                    db.SaveChanges();

                    return Redirect("/Events/Show/" + comm.EventId);
                }
                else
                {
                    return View(requestComment);
                }
            //}
            //else
            //{
            //    TempData["message"] = "Nu aveti dreptul sa faceti modificari";
            //    return RedirectToAction("Index", "Subjects");
            //}
        }

        public IActionResult Delete(int id)
        {
            Comment comm = db.Comments.Find(id);

            //if (comm.UserId == _userManager.GetUserId(User) || User.IsInRole("Editor"))
            //{
                db.Comments.Remove(comm);
                db.SaveChanges();
                return Redirect("/Events/Show/" + comm.EventId);
            //}

            //else
            //{
            //    TempData["message"] = "Nu aveti dreptul sa stergeti comentariul";
            //    return RedirectToAction("Index", "Events");
            //}
        }
    }
}
