using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using PagedList.Mvc;

namespace Forum.Models
{
    public class ThreadViewModel
    {
        public string Title { get; set; }
        public int SubForumId { get; set; }
        public Message ForumMessage { get; set; }
        public IPagedList<Message> Message { get; set; }
    }
}