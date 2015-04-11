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
    public class ForumController : Controller
    {
        //
        // GET: /Forum/
        ApplicationDbContext fe = new ApplicationDbContext();

        public ActionResult Index()
        {
            List<Forum.Models.Forum> forums = fe.Forums.ToList();
            return View(forums);
        }

        public ActionResult Details(int? id)
        {
            Forum.Models.Forum forum = fe.Forums.FirstOrDefault(f => f.ForumId == id);
            return View(forum);
        }               

        public ActionResult Search(string search, string searchBy, string sortBy, int? page)
        {
            ViewBag.SortNameParameter = string.IsNullOrEmpty(sortBy) ? "Name desc" : "";

            List<Message> SearchResults = new List<Message>();

            if (!string.IsNullOrWhiteSpace(search))
            {
                foreach (var message in fe.Messages)
                {
                    SearchResults.Add(message);
                }

                if (string.IsNullOrWhiteSpace(search) != true)
                    SearchResults = SearchResults.Where(m => m.Content.Contains(search)).ToList();

                switch (sortBy)
                {
                    case "Name desc":
                        SearchResults = SearchResults.OrderByDescending(a => a.MessageId).ToList();
                        break;
                    default:
                        SearchResults = SearchResults.OrderBy(a => a.MessageId).ToList();
                        break;
                }
            }
            

            return View(SearchResults.ToPagedList(page ?? 1, 3));
        }

    }
}
