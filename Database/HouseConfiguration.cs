using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    class HouseConfiguration : IEntityTypeConfiguration<House>
    {
        public void Configure(EntityTypeBuilder<House> builder)
        {
            builder.HasData(
                new House
                {
                    Id = 1,
                    Number = "1",
                    PosX = (decimal)33.1,
                    PosY = (decimal)33.1,
                    StreetId = 1
                },
                new House
                {
                    Id = 2,
                    Number = "3",
                    PosX = (decimal)233.1,
                    PosY = (decimal)233.1,
                    StreetId = 1
                },
                new House
                {
                    Id = 3,
                    Number = "5",
                    PosX = (decimal)253.1,
                    PosY = (decimal)253.1,
                    StreetId = 1
                },
                new House
                {
                    Id = 4,
                    Number = "1",
                    PosX = (decimal)111,
                    PosY = (decimal)233.1,
                    StreetId = 2
                },
                new House
                {
                    Id = 5,
                    Number = "2",
                    PosX = (decimal)21,
                    PosY = (decimal)233.1,
                    StreetId = 2
                },
                new House
                {
                    Id = 6,
                    Number = "4",
                    PosX = (decimal)2221,
                    PosY = (decimal)233.1,
                    StreetId = 2
                },
                new House
                {
                    Id = 7,
                    Number = "22",
                    PosX = (decimal)2521,
                    PosY = (decimal)283.1,
                    StreetId = 7
                },
                new House
                {
                    Id = 8,
                    Number = "18",
                    PosX = (decimal)521,
                    PosY = (decimal)283.1,
                    StreetId = 8
                },
                new House
                {
                    Id = 9,
                    Number = "7",
                    PosX = (decimal)241,
                    PosY = (decimal)283.1,
                    StreetId = 15
                },
                new House
                {
                    Id = 10,
                    Number = "7",
                    PosX = (decimal)21,
                    PosY = (decimal)283.1,
                    StreetId = 5
                }
                );
        }
    }
}
