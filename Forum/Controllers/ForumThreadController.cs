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
    public class ForumThreadController : Controller
    {
        //
        // GET: /ForumThread/
        ApplicationDbContext fe = new ApplicationDbContext();
            
        public ActionResult Index(int? page, int id = (int)Forum.Models.Enums.SubForums.Mainforum)
        {
            List<ForumThread> forumthreads;             
            forumthreads = fe.ForumThreads.Where(ft => ft.SubForumId == id).ToList();
            TempData["SubForumId"] = id;


            return View(forumthreads.ToPagedList(page ?? 1, 3));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ForumThread forumthread)
        {
            if(ModelState.IsValid)
            {
                //int parsedsubforumid = 0;
                //Int32.TryParse(subforumid, out parsedsubforumid);

                forumthread.CreatedOn = System.DateTime.Now;                
                fe.ForumThreads.Add(forumthread);                
                fe.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Details(int id, int? page)
        {
            ThreadViewModel tvm = new ThreadViewModel();

            List<Message> messages;

            messages = fe.Messages.Where(m => m.ForumThread.ForumThreadId == id).ToList();
            tvm.Message = messages.ToPagedList(page ?? 1, 3);            
            ViewData["ThreadId"] = id.ToString();
            tvm.Title = fe.ForumThreads.FirstOrDefault(f => f.ForumThreadId == id).Title;
            tvm.SubForumId = fe.ForumThreads.FirstOrDefault(f => f.ForumThreadId == id).SubForumId;
            tvm.ForumMessage = new Message();

            return View(tvm);
        }

    }
}
