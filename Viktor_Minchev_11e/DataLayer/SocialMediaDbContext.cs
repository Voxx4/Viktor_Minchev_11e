using BusinessLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class SocialMediaDbContext : DbContext
    {
        private SocialMediaDbContext()
        {

        }

        public SocialMediaDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-79IUFLE\SQLEXPRESS;Database=SocialMediaDB;Trusted_Connection=True;");
            }

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Interest> Interests { get; set; }

        public DbSet<Area> Areas { get; set; }

    }
}
