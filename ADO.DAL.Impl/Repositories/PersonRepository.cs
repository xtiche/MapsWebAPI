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
            try
            {
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
            catch (Exception e)
            {
                appartmentPersonRepository.RollBack();
                throw e;
            }
        }

        public override Person DefaultRowMapping(SqlDataReader reader)
        {
            return new Person
            {
                Id = (int)reader["Id"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
            };
        }

        public bool Delete(int id)
        {
            try
            {
                var res = base.ExecuteNonQuery(
                    "delete from Persons where Id = @id",
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

        public IList<Person> GetAll()
        {
            return base.ExecuteSelect("Select p.Id, p.FirstName, p.LastName from Persons p");
        }

        public Person GetById(int id)
        {
            return base.ExecuteSingleRowSelect(
                   "Select p.Id, p.FirstName, p.LastName from Persons p where p.Id = @Id",
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
            try
            {
                var newEntityId = (int)
                     base.ExecuteScalar<decimal>(
                             "insert into Persons (FirstName,LastName) values (@FirstName,@LastName) SELECT SCOPE_IDENTITY()",
                             new SqlParameters
                             {
                                 { "FirstName", entity.FirstName },
                                 { "LastName", entity.LastName }
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

        public bool RemoveAppartmentsFromPerson(int personId, IEnumerable<Appartment> appartments)
        {
            AppartmentPersonRepository appartmentPersonRepository = new AppartmentPersonRepository();
            try
            {
                foreach (var appartment in appartments)
                {
                    var existingAppartmentPerson = appartmentPersonRepository.GetById(appartment.Id, personId);
                    if (existingAppartmentPerson != null)
                        appartmentPersonRepository.Delete(appartment.Id, personId);
                }

                appartmentPersonRepository.Commit();
                return true;
            }
            catch (Exception e)
            {
                appartmentPersonRepository.RollBack();
                throw e;
            }
        }

        public override bool Update(Person entity)
        {
            try
            {
                var res = base.ExecuteNonQuery(
                        "update Persons set FirstName = @FirstName, LastName = @LastName where Id = @Id ",
                        new SqlParameters
                        {
                            { "FirstName", entity.FirstName },
                            { "LastName", entity.LastName },
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
