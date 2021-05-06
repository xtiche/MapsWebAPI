using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    class AppartmentConfiguration : IEntityTypeConfiguration<Appartment>
    {
        public void Configure(EntityTypeBuilder<Appartment> builder)
        {
            builder.HasData(
                new Appartment 
                {
                    Id = 1,
                    Number = 1,
                    HouseId = 1
                },
                new Appartment
                {
                    Id = 2,
                    Number = 2,
                    HouseId = 1
                },
                new Appartment
                {
                    Id = 3,
                    Number = 3,
                    HouseId = 1
                },
                new Appartment
                {
                    Id = 4,
                    Number = 4,
                    HouseId = 1
                },
                new Appartment
                {
                    Id = 5,
                    Number = 5,
                    HouseId = 1
                },
                new Appartment
                {
                    Id = 6,
                    Number = 6,
                    HouseId = 1
                },
                new Appartment
                {
                    Id = 7,
                    Number = 1,
                    HouseId = 2
                },
                new Appartment
                {
                    Id = 8,
                    Number = 2,
                    HouseId = 2
                },
                new Appartment
                {
                    Id = 9,
                    Number = 3,
                    HouseId = 2
                },
                new Appartment
                {
                    Id = 10,
                    Number = 1,
                    HouseId = 3
                }
            );
        }
    }
}
