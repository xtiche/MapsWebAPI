using ADO.DAL.Impl.Infrastructure;
using DAL.Abstract.Repositories;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO.DAL.Impl.Repositories
{
    public class AppartmentRepository : BaseRepository<int, Appartment>, IAppartmentRepository
    {
        public bool AddPeopleToAppartment(int appartmentId, IEnumerable<Person> people)
        {
            AppartmentPersonRepository appartmentPersonRepository = new AppartmentPersonRepository();
            foreach (var person in people)
            {
                var existingAppartmentPerson = appartmentPersonRepository.GetById(appartmentId, person.Id);
                if (existingAppartmentPerson == null)
                {
                    appartmentPersonRepository.Insert(
                        new AppartmentPerson
                        {
                            AppartmentId = appartmentId,
                            PersonId = person.Id
                        });
                }
            }
            appartmentPersonRepository.Commit();

            return true;
        }

        public override Appartment DefaultRowMapping(SqlDataReader reader)
        {
            return new Appartment
            {
                Id = (int)reader["Id"],
                Number = (int)reader["Number"],
                HouseId = (int)reader["HouseId"]
            };
        }

        public bool Delete(int id)
        {
            var res = base.ExecuteNonQuery(
                "delete from Appartments where Id = @id",
                new SqlParameters() { { "id", id } });

            base.Commit();

            return res == 1;
        }

        public IList<Appartment> GetAll()
        {
            return base.ExecuteSelect("Select a.Id, a.Number, a.HouseId from Appartments a");
        }
        public IList<Appartment> GetAppartmentsByHouseId(int houseId)
        {
            return base.ExecuteSelect("Select a.Id, a.Number, a.HouseId from Appartments a where HouseId = @HouseId",
                new SqlParameters()
                   {
                        {"HouseId",houseId}
                   });
        }

        public Appartment GetById(int id)
        {
            return base.ExecuteSingleRowSelect(
                   "Select a.Id, a.Number, a.HouseId from Appartments a where a.Id = @Id",
                   new SqlParameters()
                   {
                        { "Id", id }
                   }
               );
        }

        public List<Person> GetPeopleInAppartment(int appartmentId)
        {
            AppartmentPersonRepository appartmentPersonRepository = new AppartmentPersonRepository();
            var appartmentPersons = appartmentPersonRepository.GetRelationByAppartmentId(appartmentId);

            var people = new List<Person>();
            PersonRepository personRepository = new PersonRepository();
            foreach (var appartmentPerson in appartmentPersons)
                people.Add(personRepository.GetById(appartmentPerson.PersonId));

            return people;
        }

        public override int Insert(Appartment entity)
        {
            var newEntityId = (int)
                 base.ExecuteScalar<decimal>(
                         "insert into Appartments (Number,HouseId) values (@Number,@HouseId) SELECT SCOPE_IDENTITY()",
                         new SqlParameters
                         {
                            { "Number", entity.Number },
                            { "HouseId", entity.HouseId }
                         }
                     );
            base.Commit();
            return newEntityId;
        }

        public bool RemovePeopleFromAppartment(int appartmentId, IEnumerable<Person> people)
        {
            AppartmentPersonRepository appartmentPersonRepository = new AppartmentPersonRepository();
            foreach (var person in people)
            {
                var existingAppartmentPerson = appartmentPersonRepository.GetById(appartmentId, person.Id);
                if (existingAppartmentPerson != null)
                    appartmentPersonRepository.Delete(appartmentId, person.Id);

            }
            appartmentPersonRepository.Commit();

            return true;
        }

        public override bool Update(Appartment entity)
        {
            var res = base.ExecuteNonQuery(
                    "update Appartments set Number = @Number, HouseId = @HouseId where Id = @Id ",
                    new SqlParameters
                    {
                        { "Number", entity.Number },
                        { "HouseId", entity.HouseId },
                        { "Id", entity.Id }
                    }
                );

            base.Commit();
            return res > 0;
        }
    }
}
