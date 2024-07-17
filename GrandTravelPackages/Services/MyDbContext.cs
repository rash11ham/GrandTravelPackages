using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//...
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GrandTravelPackages.Models;

namespace GrandTravelPackages.Services
{
    public class MyDbContext:IdentityDbContext
    {
        public DbSet<Customer> CustomerTbl { get; set; }
        public DbSet<TravelProvider> TravelProviderTbl { get; set; }
        public DbSet<Package> PackageTbl { get; set; }
        public DbSet<Order> OrderTbl { get; set; }
        public DbSet<Feedback> FeedbackTbl { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<CustomerProvider>()
        //        .HasKey(t => new { t.CustomerId, t.TravelProviderId });
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder option)
        {
            //option.UseSqlServer(@"Server=tcp:getawaytravels.database.windows.net,1433;Initial Catalog=GetAwayTravels;Persist Security Info=False;User ID=Rashed;Password=Keys=123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            option.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB ; Database=DbGTP; Trusted_Connection=True");
        }
       
    }
}
