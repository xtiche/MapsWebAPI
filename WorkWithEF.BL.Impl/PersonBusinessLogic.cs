using Common.BL.Abstract;
using Database;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WorkWithEF.BL.Impl
{
    public class PersonBusinessLogic : IPersonBusinessLogic
    {
        private readonly ApplicationContext _applicationContext;

        public PersonBusinessLogic(ApplicationContext context)
        {
            _applicationContext = context;
        }

        public IEnumerable<Person> GetPeopleByLastName(string lastName)
        {
            return _applicationContext.Persons.Where(p => EF.Functions.Like(p.LastName, "%" + lastName + "%"));
        
        }
    }
}
