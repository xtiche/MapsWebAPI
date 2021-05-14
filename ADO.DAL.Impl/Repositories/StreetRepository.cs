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
    public class StreetRepository : BaseRepository<int, Street>, IStreetRepository
    {
        public bool AddHousesToStreet(int streetId, IEnumerable<House> houses)
        {
            try
            {
                int res = 0;
                foreach (House house in houses)
                {
                    res += base.ExecuteNonQuery(
                            "update Houses set StreetId = @StreetId where Id = @Id ",
                            new SqlParameters
                            {
                                {"StreetId", streetId},
                                {"Id", house.Id}
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

        public override Street DefaultRowMapping(SqlDataReader reader)
        {
            return new Street
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"],
                CityId = (int)reader["CityId"]
            };
        }

        public bool Delete(int id)
        {
            try
            {
                var res = base.ExecuteNonQuery(
                    "delete from Streets where Id = @id",
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

        public IList<Street> GetAll()
        {
            var streets = base.ExecuteSelect("Select s.Id, s.Name, s.CityId from Streets s");

            HouseRepository houseRepository = new HouseRepository();
            foreach (var street in streets)
                street.Houses = houseRepository.GetHousesByStreetId(street.Id).ToList();

            return streets;
        }

        public Street GetById(int id)
        {
            var street = base.ExecuteSingleRowSelect(
                    "select s.Id, s.Name from Streets s where s.Id = @Id",
                    new SqlParameters()
                    {
                        {"Id",id}
                    }
                );

            HouseRepository houseRepository = new HouseRepository();
            street.Houses = houseRepository.GetHousesByStreetId(street.Id).ToList();

            return street;
        }

        public IList<Street> GetStreetsByCityId(int cityId)
        {
            return base.ExecuteSelect("Select s.Id, s.Name, s.CityId from Streets s where s.CityId = @cityId",
                new SqlParameters()
                    {
                        {"cityId",cityId}
                    });
        }

        public override int Insert(Street entity)
        {
            try
            {
                var newEntityId = (int)
                     base.ExecuteScalar<decimal>(
                             "insert into Streets (Name,CityId) values (@Name,@CityId) SELECT SCOPE_IDENTITY()",
                             new SqlParameters
                             {
                                { "Name", entity.Name },
                                { "CityId", entity.CityId }
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

        public override bool Update(Street entity)
        {
            try
            {
                var res = base.ExecuteNonQuery(
                        "update Streets set Name = @Name, CityId = @CityId where Id = @Id ",
                        new SqlParameters
                        {
                            {"Name", entity.Name},
                            {"CityId", entity.CityId},
                            {"Id", entity.Id}
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
