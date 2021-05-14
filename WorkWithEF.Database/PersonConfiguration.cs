using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasData(
                new Person
                {
                    Id = 1,
                    FirstName = "Ronald",
                    LastName = "Dalton"
                },
                new Person
                {
                    Id = 2,
                    FirstName = "Richard",
                    LastName = "Ashton"
                },
                new Person
                {
                    Id = 3,
                    FirstName = "Jason",
                    LastName = "Griffin"
                },
                new Person
                {
                    Id = 4,
                    FirstName = "Richard",
                    LastName = "Rivera"
                },
                new Person
                {
                    Id = 5,
                    FirstName = "Charles",
                    LastName = "Richardson"
                },
                new Person
                {
                    Id = 6,
                    FirstName = "Seth",
                    LastName = "Cook"
                },
                new Person
                {
                    Id = 7,
                    FirstName = "Kaylee",
                    LastName = "Reed"
                },
                new Person
                {
                    Id = 8,
                    FirstName = "Jennifer",
                    LastName = "Lopez"
                },
                new Person
                {
                    Id = 9,
                    FirstName = "Alexandra",
                    LastName = "Parker"
                },
                new Person
                {
                    Id = 10,
                    FirstName = "Irea",
                    LastName = "Rogers"
                },
                new Person
                {
                    Id = 11,
                    FirstName = "Jennifer",
                    LastName = "Flores"
                }
                );
        }
    }
}
