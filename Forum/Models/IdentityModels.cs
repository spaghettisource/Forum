using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Forum.Models
{

    [Table("Forum")]
    public class Forum
    {
        public int ForumId { get; set; }
        public string ForumTitle { get; set; }

        public virtual List<SubForum> SubForum { get; set; }
    }

    public class SubForum
    {
        public int SubForumId { get; set; }
        public string Title { get; set; }
        
        public virtual List<ForumThread> ForumThreadId { get; set; }
        public virtual Forum Forum { get; set; }
    }

    public class ForumThread
    {
        public int ForumThreadId { get; set; }
        public string Title { get; set; }
        public List<Message> MessageId { get; set; }
        public DateTime CreatedOn { get; set; }
        //public int UserId { get; set; }
        public int SubForumId { get; set; }

        public virtual SubForum SubForum { get; set; }
        public virtual ApplicationUser User { get; set; }
        public virtual Forum Forum { get; set; }
    }

    public class Message
    {
        public int MessageId { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }
        //  public int UserId { get; set; }
        // public int ForumThreadId { get; set; }

        public virtual ForumThread ForumThread { get; set; }     
        public virtual ApplicationUser User { get; set; }
    }

    


    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("ForumEntities", throwIfV1Schema: false)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<SubForum> SubForums { get; set; }        
        public DbSet<ForumThread> ForumThreads { get; set; }
        public DbSet<Forum> Forums { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


    }
}