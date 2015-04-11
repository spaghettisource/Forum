using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;

namespace Forum.Controllers
{
    public class UserController : Controller
    {
        //
        // GET: /User/
        ApplicationDbContext fe = new ApplicationDbContext();

        public ActionResult Index(string search, string searchBy, string sortBy, int? page)
        {
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";

            List<string> ForumUsers = new List<string>();
            foreach(var user in fe.Users)
            {
                ForumUsers.Add(user.UserName);
            }

            if(string.IsNullOrEmpty(search) != true)
            ForumUsers = ForumUsers.Where(u => u.StartsWith(search)).ToList();

            switch (sortBy)
            {
                case "Name desc":
                 ForumUsers = ForumUsers.OrderByDescending(a => a).ToList();
                 break;
                default:
                 ForumUsers = ForumUsers.OrderBy(a => a).ToList();
                 break;
            }
                

            return View(ForumUsers.ToPagedList(page ?? 1, 3));
        }

    }
}
