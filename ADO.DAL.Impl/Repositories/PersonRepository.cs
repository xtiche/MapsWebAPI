using ADO.DAL.Impl.Infrastructure;
using DAL.Abstract.Repositories;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO.DAL.Impl.Repositories
{
    public class PersonRepository : BaseRepository<int, Person>, IPersonRepository
    {
        public bool AddAppartmentsToPerson(int personId, IEnumerable<Appartment> appartments)
        {
            AppartmentPersonRepository appartmentPersonRepository = new AppartmentPersonRepository();
            foreach (var appartment in appartments)
            {
                var existingAppartmentPerson = appartmentPersonRepository.GetById(appartment.Id, personId);
                if (existingAppartmentPerson == null)
                {
                    appartmentPersonRepository.Insert(
                        new AppartmentPerson
                        {
                            AppartmentId = appartment.Id,
                            PersonId = personId
                        });
                }
            }
            appartmentPersonRepository.Commit();

            return true;
        }

        public override Person DefaultRowMapping(SqlDataReader reader)
        {
            return new Person
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"]
            };
        }

        public bool Delete(int id)
        {
            var res = base.ExecuteNonQuery(
                "delete from Persons where Id = @id",
                new SqlParameters() { { "id", id } });

            base.Commit();

            return res == 1;
        }

        public IList<Person> GetAll()
        {
            return base.ExecuteSelect("Select p.Id, p.Name from Persons p");
        }

        public Person GetById(int id)
        {
            return base.ExecuteSingleRowSelect(
                   "Select p.Id, p.Name from Persons p where p.Id = @Id",
                   new SqlParameters()
                   {
                        { "Id", id }
                   }
               );
        }

        public List<Appartment> GetPersonsAppartments(int personId)
        {
            AppartmentPersonRepository appartmentPersonRepository = new AppartmentPersonRepository();
            var appartmentPersons = appartmentPersonRepository.GetRelationByPersonId(personId);

            var appartments = new List<Appartment>();
            AppartmentRepository appartmentRepository = new AppartmentRepository();
            foreach (var appartmentPerson in appartmentPersons)
                appartments.Add(appartmentRepository.GetById(appartmentPerson.AppartmentId));

            return appartments;
        }

        public override int Insert(Person entity)
        {
            var newEntityId = (int)
                 base.ExecuteScalar<decimal>(
                         "insert into Persons (Name) values (@Name) SELECT SCOPE_IDENTITY()",
                         new SqlParameters
                         {
                            { "Name", entity.Name }
                         }
                     );
            base.Commit();
            return newEntityId;
        }

        public bool RemoveAppartmentsFromPerson(int personId, IEnumerable<Appartment> appartments)
        {
            AppartmentPersonRepository appartmentPersonRepository = new AppartmentPersonRepository();
            foreach (var appartment in appartments)
            {
                var existingAppartmentPerson = appartmentPersonRepository.GetById(appartment.Id, personId);
                if (existingAppartmentPerson != null)
                    appartmentPersonRepository.Delete(appartment.Id, personId);
            }
            appartmentPersonRepository.Commit();

            return true;
        }

        public override bool Update(Person entity)
        {
            var res = base.ExecuteNonQuery(
                    "update Persons set Name = @Name where Id = @Id ",
                    new SqlParameters
                    {
                        { "Name", entity.Name },
                        { "Id", entity.Id }
                    }
                );

            base.Commit();
            return res > 0;
        }
    }
}
