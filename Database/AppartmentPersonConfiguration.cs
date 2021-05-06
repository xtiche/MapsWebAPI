using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    class AppartmentPersonConfiguration : IEntityTypeConfiguration<AppartmentPerson>
    {
        public void Configure(EntityTypeBuilder<AppartmentPerson> builder)
        {
            builder.HasData(
                new AppartmentPerson
                {
                    AppartmentId = 1,
                    PersonId = 1
                },
                new AppartmentPerson
                {
                    AppartmentId = 1,
                    PersonId = 11
                },
                new AppartmentPerson
                {
                    AppartmentId = 1,
                    PersonId = 10
                },
                new AppartmentPerson
                {
                    AppartmentId = 2,
                    PersonId = 2
                },
                new AppartmentPerson
                {
                    AppartmentId = 2,
                    PersonId = 8
                },
                new AppartmentPerson
                {
                    AppartmentId = 3,
                    PersonId = 3
                },
                new AppartmentPerson
                {
                    AppartmentId = 3,
                    PersonId = 7
                },
                new AppartmentPerson
                {
                    AppartmentId = 3,
                    PersonId = 6
                },
                new AppartmentPerson
                {
                    AppartmentId = 4,
                    PersonId = 5
                });
        }
    }
}
