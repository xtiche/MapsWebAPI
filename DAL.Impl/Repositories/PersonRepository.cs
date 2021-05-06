using DAL.Abstract.Repositories;
using Database;
using Entity.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Impl.Repositories
{
    class PersonRepository : IPersonRepository
    {
        private readonly ApplicationContext _applicationContext;

        public PersonRepository(ApplicationContext context)
        {
            _applicationContext = context;
        }

        public bool Delete(int id)
        {
            Person existingPerson = _applicationContext.Persons.Find(id);
            if (existingPerson == null)
                throw new ArgumentNullException(nameof(existingPerson));

            _applicationContext.Remove(existingPerson);
            _applicationContext.SaveChanges();

            return true;
        }

        public IList<Person> GetAll()
        {
            return _applicationContext.Persons.ToList();
        }

        public Person GetById(int id)
        {
            return _applicationContext.Persons.Find(id);
        }

        public int Insert(Person entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _applicationContext.Persons.Add(entity);
            _applicationContext.SaveChanges();

            return entity.Id;
        }

        public bool Update(Person entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (!_applicationContext.Countries.Any(c => c.Id == entity.Id))
                throw new ArgumentNullException(nameof(entity));

            _applicationContext.Update(entity);
            _applicationContext.SaveChanges();

            return true;
        }
    }
}
