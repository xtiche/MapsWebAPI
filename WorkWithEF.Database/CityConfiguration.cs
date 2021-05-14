using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Database
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasData(
                new City
                {
                    Id = 1,
                    Name = "Kiev",
                    CountryId = 1
                },
                new City
                {
                    Id = 2,
                    Name = "Kharkiv",
                    CountryId = 1
                },
                new City
                {
                    Id = 3,
                    Name = "Odesa",
                    CountryId = 1
                },
                new City
                {
                    Id = 4,
                    Name = "Dnipro",
                    CountryId = 1
                },

                 new City
                 {
                     Id = 10,
                     Name = "Moscow",
                     CountryId = 2
                 },
                 new City
                 {
                     Id = 11,
                     Name = "Saint Petersburg",
                     CountryId = 2
                 },
                 new City
                 {
                     Id = 12,
                     Name = "Novosibirsk",
                     CountryId = 2
                 },
                 new City
                 {
                     Id = 30,
                     Name = "Warsaw",
                     CountryId = 3
                 },
                 new City
                 {
                     Id = 31,
                     Name = "Kraków",
                     CountryId = 3
                 }
                );
        }
    }
}
