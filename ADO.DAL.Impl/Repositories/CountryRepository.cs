using ADO.DAL.Impl.Infrastructure;
using DAL.Abstract.Repositories;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ADO.DAL.Impl.Repositories
{
    public class CountryRepository : BaseRepository<int, Country>, ICountryRepository
    {
        public bool AddCitiesToCountry(int countryId, IEnumerable<City> cities)
        {
            int res = 0;
            foreach (City city in cities)
            {
                res += base.ExecuteNonQuery(
                        "update Cities set CountryId = @CountryId where Id = @Id ",
                        new SqlParameters
                        {
                            {"CountryId", countryId},
                            {"Id", city.Id}
                        }
                    );
            }

            base.Commit();
            return res > 0;
        }

        public override Country DefaultRowMapping(SqlDataReader reader)
        {
            return new Country
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"]
            };
        }

        public bool Delete(int id)
        {
            var res = base.ExecuteNonQuery(
                "delete from Countries where Id = @id",
                new SqlParameters() { { "id", id } });

            base.Commit();

            return res == 1;
        }

        public IList<Country> GetAll()
        {
            var countries = base.ExecuteSelect("Select c.Id, c.Name from Countries c");

            CityRepository cityRepository = new CityRepository();
            foreach (var country in countries)
                country.Cities = cityRepository.GetCitiesByCountryId(country.Id).ToList();

            return countries;
        }

        public Country GetById(int id)
        {
            var country = base.ExecuteSingleRowSelect(
                    "select c.Id, c.Name from Countries c where c.Id = @Id",
                    new SqlParameters()
                    {
                        {"Id",id}
                    }
                );

            CityRepository cityRepository = new CityRepository();
            country.Cities = cityRepository.GetCitiesByCountryId(id).ToList();

            return country;
        }

        public override int Insert(Country entity)
        {
            var newEntityId = (int)
                base.ExecuteScalar<decimal>(
                        "insert into Countries (Name) values (@Name) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            {"Name", entity.Name}
                        }
                    );
            base.Commit();
            return newEntityId;
        }

        public override bool Update(Country entity)
        {
            var res = base.ExecuteNonQuery(
                    "update Countries set Name = @Name where Id = @Id ",
                    new SqlParameters
                    {
                        {"Name", entity.Name},
                        {"Id", entity.Id}
                    }
                );

            base.Commit();
            return res > 0;
        }
    }
}
