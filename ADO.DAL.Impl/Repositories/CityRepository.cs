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
    public class CityRepository : BaseRepository<int, City>, ICityRepository
    {
        public bool AddStreetsToCity(int cityId, IEnumerable<Street> streets)
        {
            try
            {
                int res = 0;
                foreach (Street street in streets)
                {
                    res += base.ExecuteNonQuery(
                            "update Streets set CityId = @CityId where Id = @Id ",
                            new SqlParameters
                            {
                                {"CityId", cityId},
                                {"Id", street.Id}
                            }
                        );
                }

                base.Commit();
                return res > 0;
            }
            catch (Exception e)
            {
                base.RollBack();
                throw e;
            }
        }

        public override City DefaultRowMapping(SqlDataReader reader)
        {
            return new City
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                CountryId = (int)reader["CountryId"]
            };
        }

        public bool Delete(int id)
        {
            try
            {
                var res = base.ExecuteNonQuery(
                    "delete from Cities where Id = @id",
                    new SqlParameters() { { "id", id } });

                base.Commit();
                return res == 1;
            }
            catch (Exception e)
            {
                base.RollBack();
                throw e;
            }
        }

        public IList<City> GetAll()
        {
            var cities = base.ExecuteSelect("Select c.Id, c.Name, c.CountryId from Cities c");

            StreetRepository streetRepository = new StreetRepository();
            foreach (var city in cities)
                city.Streets = streetRepository.GetStreetsByCityId(city.Id).ToList();

            return cities;
        }

        public City GetById(int id)
        {
            var city = base.ExecuteSingleRowSelect("Select c.Id, c.Name, c.CountryId from Cities c where c.Id = @Id",
                new SqlParameters()
                    {
                        {"Id",id}
                    });

            StreetRepository streetRepository = new StreetRepository();
            city.Streets = streetRepository.GetStreetsByCityId(city.Id).ToList();

            return city;
        }

        public IList<City> GetCitiesByCountryId(int countryId)
        {
            return base.ExecuteSelect("Select c.Id, c.Name, c.CountryId from Cities c where c.CountryId = @countryId ",
                new SqlParameters()
                    {
                        {"countryId",countryId}
                    });
        }

        public override int Insert(City entity)
        {
            try
            {
                var newEntityId = (int)
                    base.ExecuteScalar<decimal>(
                            "insert into Cities (Name,CountryId) values (@Name,@CountryId) SELECT SCOPE_IDENTITY()",
                            new SqlParameters
                            {
                                { "Name", entity.Name },
                                { "CountryId", entity.CountryId }
                            }
                        );
                base.Commit();
                return newEntityId;
            }
            catch (Exception e)
            {
                base.RollBack();
                throw e;
            }
        }


        public override bool Update(City entity)
        {
            try
            {
                var res = base.ExecuteNonQuery(
                        "update Cities set Name = @Name, CountryId = @CountryId where Id = @Id ",
                        new SqlParameters
                        {
                            { "Name", entity.Name },
                            { "CountryId", entity.CountryId },
                            { "Id", entity.Id }
                        }
                    );

                base.Commit();
                return res > 0;
            }
            catch (Exception e)
            {
                base.RollBack();
                throw e;
            }
        }
    }
}
