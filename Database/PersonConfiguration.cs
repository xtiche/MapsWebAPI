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
                    Name = "Ronald Dalton"
                },
                new Person
                {
                    Id = 2,
                    Name = "Ashton Richard Gray"
                },
                new Person
                {
                    Id = 3,
                    Name = "Brandon Jason Griffin"
                },
                new Person
                {
                    Id = 4,
                    Name = "Bryan Richard Rivera"
                },
                new Person
                {
                    Id = 5,
                    Name = "Jeremiah Charles Richardson"
                },
                new Person
                {
                    Id = 6,
                    Name = "Ryan Seth Cook"
                },
                new Person
                {
                    Id = 7,
                    Name = "Jennifer Kaylee Reed"
                },
                new Person
                {
                    Id = 8,
                    Name = "Amber Jennifer Lopez"
                },
                new Person
                {
                    Id = 9,
                    Name = "Alexandra Ashley Parker"
                },
                new Person
                {
                    Id = 10,
                    Name = "Taylor Irea Rogers"
                }, new Person
                {
                    Id = 11,
                    Name = "Jennifer Erin Flores"
                }
                );
        }
    }
}
