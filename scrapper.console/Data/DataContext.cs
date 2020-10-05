using Microsoft.EntityFrameworkCore;
using scrapper.console.Data.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace scrapper.console.Data
{
    public class DataContext : DbContext
    {
        //public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Offer> Offers { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=localhost;Port=5432;Database=ScrapOffers;User Id=postgres;Password=Temporal1;";
            optionsBuilder.UseNpgsql($"{connectionString}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OfferModelBuilder());
            
            base.OnModelCreating(modelBuilder);
        }

    }
}
