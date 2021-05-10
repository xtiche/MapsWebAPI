using Common.BL.Abstract;
using Database;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkWithEF.BL.Impl
{
    public class HouseBusinessLogic : IHouseBusinessLogic
    {
        private readonly ApplicationContext _applicationContext;

        public HouseBusinessLogic(ApplicationContext context)
        {
            _applicationContext = context;
        }

        public IEnumerable<Person> GetPeopleFromHouse(int houseId)
        {
            List<Person> people = new List<Person>();
            var appartments = _applicationContext.Appartments.Where(ap => ap.HouseId == houseId).ToList();
            foreach (var appartment in appartments)
            {
                var appartmentPersons = _applicationContext.AppartmentPersons.Where(ap => ap.AppartmentId == appartment.Id).ToList();
                foreach (var appartmentPerson in appartmentPersons)
                {
                    var person = _applicationContext.Persons.Find(appartmentPerson.PersonId);
                    if (!people.Contains(person))
                        people.Add(person);
                }
            }
            return people;
        }
    }
}
