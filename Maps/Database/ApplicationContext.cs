using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Maps.Models;
using Microsoft.EntityFrameworkCore;

namespace Maps.Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Street> Streets { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Appartment> Appartments { get; set; }
        public DbSet<Person> Pesrons { get; set; }
        public DbSet<AppartmentPerson> AppartmentPesrons { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppartmentPerson>()
                .HasKey(sc => new { sc.AppartmentId, sc.PersonId });

            modelBuilder.ApplyConfiguration(new CountryConfiguration());
            modelBuilder.ApplyConfiguration(new CityConfiguration());
        }
    }
}
