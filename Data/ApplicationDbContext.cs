using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NoteDemoMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoteDemoMVC
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Status> Status { get; set; }

        public DbSet<Note> Note{ get; set; } 

        public DbSet<FileData> FileData { get; set; } 

        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
