using ADO.DAL.Impl.Infrastructure;
using ADO.DAL.Impl.Repositories;
using Common.BL.Abstract;
using Entity.Models;
using System;
using System.Collections.Generic;

namespace ADO.BL.Impl
{
    public class PersonBusinessLogic : IPersonBusinessLogic
    {
        public IEnumerable<Person> GetPeopleByLastName(string lastName)
        {
            PersonRepository personRepository = new PersonRepository();
            return personRepository.ExecuteSelect("Select p.Id, p.FirstName, p.LastName from Persons p where p.LastName LIKE @LastName",
                   new SqlParameters()
                   {
                        { "LastName", "%" + lastName + "%" }
                   });
        }
    }
}
