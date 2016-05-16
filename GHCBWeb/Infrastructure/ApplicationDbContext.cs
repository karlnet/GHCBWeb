using GHCBWeb.Data.Entities;
using GHCBWeb.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GHCBWeb.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public  DbSet<App> Apps { get; set; }      
        public  DbSet<Board> Boards { get; set; }
        public  DbSet<BoardPort> BoardPorts { get; set; }
        public  DbSet<PortDescription> PortDescriptions { get; set; }
        public  DbSet<ApplicationUserBoard> ApplicationUserBoards { get; set; }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    var applicationUserBoards = modelBuilder.Entity<ApplicationUserBoard>();
        //    applicationUserBoards.HasKey(c => c.Id);
        //    //applicationUserBoards.HasRequired(c => c.applicationUser).WithMany().Map(s => { s.MapKey("ApplicationUserName"); });
        //    //applicationUserBoards.HasRequired(c => c.board).WithMany().Map(s => s.MapKey("BoardId"));
        //}

    }
}