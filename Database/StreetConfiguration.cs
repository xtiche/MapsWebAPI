using Entity.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    class StreetConfiguration : IEntityTypeConfiguration<Street>
    {
        public void Configure(EntityTypeBuilder<Street> builder)
        {
            builder.HasData(
            #region ukraine
                new Street
                {
                    Id = 1,
                    Name = "Academic",
                    CityId = 1
                },
                new Street
                {
                    Id = 2,
                    Name = "Blossom",
                    CityId = 1
                },
                new Street
                {
                    Id = 3,
                    Name = "Bogdan Khmelnitsky",
                    CityId = 1
                },

                new Street
                {
                    Id = 4,
                    Name = "Sumska",
                    CityId = 2
                },
                new Street
                {
                    Id = 5,
                    Name = "Andriyivskyy Descent",
                    CityId = 2
                },

                new Street
                {
                    Id = 6,
                    Name = "Volodymyrska",
                    CityId = 3
                },
            #endregion

            #region russia
                new Street
                {
                    Id = 7,
                    Name = "Academic",
                    CityId = 10
                },
                new Street
                {
                    Id = 8,
                    Name = "Blossom",
                    CityId = 10
                },
                new Street
                {
                    Id = 9,
                    Name = "Bogdan Khmelnitsky",
                    CityId = 10
                },


                new Street
                {
                    Id = 10,
                    Name = "Sumska",
                    CityId = 11
                },
                new Street
                {
                    Id = 11,
                    Name = "Andriyivskyy Descent",
                    CityId = 11
                },


                new Street
                {
                    Id = 12,
                    Name = "Volodymyrska",
                    CityId = 12
                },
            #endregion

            #region poland
                new Street
                {
                    Id = 13,
                    Name = "Academic",
                    CityId = 30
                },
                new Street
                {
                    Id = 14,
                    Name = "Blossom",
                    CityId = 30
                },
                new Street
                {
                    Id = 15,
                    Name = "Bogdan Khmelnitsky",
                    CityId = 30
                },

                new Street
                {
                    Id = 16,
                    Name = "Sumska",
                    CityId = 30
                },

                new Street
                {
                    Id = 17,
                    Name = "Andriyivskyy Descent",
                    CityId = 31
                },


                new Street
                {
                    Id = 18,
                    Name = "Volodymyrska",
                    CityId = 31
                }
                #endregion
            );
        }
    }
}
