using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<House> Streets { get; set; }
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
            modelBuilder.ApplyConfiguration(new StreetConfiguration());
            modelBuilder.ApplyConfiguration(new HouseConfiguration());
            modelBuilder.ApplyConfiguration(new AppartmentConfiguration());
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
            modelBuilder.ApplyConfiguration(new AppartmentPersonConfiguration());
        }
    }
}
