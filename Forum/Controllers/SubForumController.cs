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
    public class SubForumController : Controller
    {
        //
        // GET: /SubForum/
        ApplicationDbContext forumcontext = new ApplicationDbContext();

        public ActionResult Index(int? id, int? page)
        {
            IPagedList<Forum.Models.ForumThread> forumthreads = forumcontext.ForumThreads.Where(f => f.ForumThreadId == id).ToPagedList(page ?? 1, 3);

            return View(forumthreads);
        }

    }
}
