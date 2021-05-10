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
    public class HouseRepository : BaseRepository<int, House>, IHouseRepository
    {
        public bool AddAppartmentsToHouse(int houseId, IEnumerable<Appartment> appartments)
        {
            int res = 0;
            foreach (Appartment appartment in appartments)
            {
                res += base.ExecuteNonQuery(
                        "update Appartments set HouseId = @HouseId where Id = @Id ",
                        new SqlParameters
                        {
                            {"HouseId", houseId},
                            {"Id", appartment.Id}
                        }
                    );
            }

            base.Commit();
            return res > 0;
        }

        public override House DefaultRowMapping(SqlDataReader reader)
        {
            return new House
            {
                Id = (int)reader["Id"],
                Number = (string)reader["Number"],
                PosX = (decimal)reader["PosX"],
                PosY = (decimal)reader["PosY"],
                StreetId = (int)reader["StreetId"]
            };
        }

        public bool Delete(int id)
        {
            var res = base.ExecuteNonQuery(
                "delete from Houses where Id = @id",
                new SqlParameters() { { "id", id } });

            base.Commit();

            return res == 1;
        }

        public IList<House> GetAll()
        {
            var houses = base.ExecuteSelect("Select h.Id, h.Number, h.PosX h.PosY, h.StreetId from Houses h");

            AppartmentRepository appartmentRepository = new AppartmentRepository();
            foreach (var house in houses)
                house.Appartments = appartmentRepository.GetAppartmentsByHouseId(house.Id).ToList();

            return houses;
        }

        public House GetById(int id)
        {
            var house = base.ExecuteSingleRowSelect("Select h.Id, h.Number, h.PosX h.PosY, h.StreetId from Houses h where h.Id = @Id",
                new SqlParameters()
                    {
                        {"Id",id}
                    });

            AppartmentRepository appartmentRepository = new AppartmentRepository();
            house.Appartments = appartmentRepository.GetAppartmentsByHouseId(house.Id).ToList();

            return house;
        }
        public IList<House> GetHousesByStreetId(int streetId)
        {
            return base.ExecuteSelect("Select h.Id, h.Number, h.PosX h.PosY, h.StreetId from Houses h where h.StreetId = @streetId",
                new SqlParameters()
                    {
                        {"streetId",streetId}
                    });
        }

        public override int Insert(House entity)
        {
            var newEntityId = (int)
                base.ExecuteScalar<decimal>(
                        "insert into Houses (Number,PosX,PosY,StreetId) values (@Number,@PosX,@PosY,@StreetId) SELECT SCOPE_IDENTITY()",
                        new SqlParameters
                        {
                            { "Number", entity.Number },
                            { "PosX", entity.PosX },
                            { "PosY", entity.PosY },
                            { "StreetId", entity.StreetId }
                        }
                    );
            base.Commit();
            return newEntityId;
        }

        public override bool Update(House entity)
        {
            var res = base.ExecuteNonQuery(
                    "update Houses set Number = @Number, PosX = @PosX, PosY = @PosY, StreetId = @StreetId " +
                    "where Id = @Id ",
                    new SqlParameters
                    {
                        { "Number", entity.Number },
                        { "PosX", entity.PosX },
                        { "PosY", entity.PosY },
                        { "StreetId", entity.StreetId },
                        { "Id", entity.Id}
                    }
                );

            base.Commit();
            return res > 0;
        }
    }
}
