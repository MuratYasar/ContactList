using Entities.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class ContactListContext : DbContext
    {
        public ContactListContext(DbContextOptions<ContactListContext> options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<ContactDetail> ContactDetails { get; set; }

        public DbSet<Log> Logs { get; set; }

        public DbSet<Report> Reports { get; set; }

        public DbSet<ReportStatus> ReportStatus { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=ContactListDB;Integrated Security=true; User Id=postgres;Password=muratyasar;Pooling=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<Contact>().ToTable("Contact", "public");
            modelBuilder.Entity<ContactDetail>().ToTable("ContactDetail", "public");
            modelBuilder.Entity<Log>().ToTable("Log", "public");
            modelBuilder.Entity<Report>().ToTable("Report", "public");
            modelBuilder.Entity<ReportStatus>().ToTable("ReportStatus", "public");

            base.OnModelCreating(modelBuilder);
        }

    }
}
