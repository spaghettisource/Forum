using Forum.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forum.Controllers
{
    public class MessageController : Controller
    {
        //
        // GET: /Messages/
        ApplicationDbContext fe = new ApplicationDbContext();

        public ActionResult Index()
        {            
            var results = fe.Messages.FirstOrDefault();
            return View(results);
        }

        [HttpGet]
        public ActionResult Create(int id)
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Message msg, string id)
        {
            if(ModelState.IsValid)
            {
                int parsedid =0;
                Int32.TryParse(id, out parsedid);
                msg.Created = System.DateTime.Now;
                msg.User = fe.Users.Single(u => u.UserName == User.Identity.Name);
                msg.ForumThread =  fe.ForumThreads.Single(f => f.ForumThreadId == parsedid);
                fe.Messages.Add(msg);
                fe.SaveChanges();
                return RedirectToAction("Index", "ForumThread", new { id = id });
            }
            return View();
        }

    }
}
