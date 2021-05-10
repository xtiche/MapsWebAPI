using ADO.DAL.Impl.Infrastructure;
using ADO.DAL.Impl.Repositories;
using Common.BL.Abstract;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ADO.BL.Impl
{
    public class HouseBusinessLogic : IHouseBusinessLogic
    {
        public IEnumerable<Person> GetPeopleFromHouse(int houseId)
        {
            PersonRepository personRepository = new PersonRepository();
            return personRepository.ExecuteSelect(
                "Select* from Persons as p " +
                "left join AppartmentPersons as ap on ap.PersonId = p.Id " +
                "left join Appartments as a on a.Id = ap.AppartmentId " +
                "where a.HouseId = @HouseId",
                   new SqlParameters()
                   {
                        { "HouseId", houseId}
                   });
        }
    }
}
