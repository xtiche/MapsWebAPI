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
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationContext _applicationContext;

        public PersonRepository(ApplicationContext context)
        {
            _applicationContext = context;
        }

        public bool AddAppartmentsToPerson(int personId, IEnumerable<Appartment> appartments)
        {
            if (appartments == null)
                throw new ArgumentNullException(nameof(appartments));

            var existingPerson = _applicationContext.Persons.Find(personId);
            if (existingPerson == null)
                throw new ArgumentNullException(nameof(existingPerson));

            foreach (var appartment in appartments)
            {
                var existingAppartment = _applicationContext.Appartments.Find(appartment.Id);
                if (existingAppartment == null)
                    throw new ArgumentNullException(nameof(existingAppartment));

                if (!_applicationContext.AppartmentPersons
                    .Any(x => x.AppartmentId == existingAppartment.Id && x.PersonId == existingAppartment.Id))
                {
                    _applicationContext.AppartmentPersons.Add(
                        new AppartmentPerson
                        {
                            AppartmentId = existingAppartment.Id,
                            PersonId = existingAppartment.Id
                        });
                }
            }
            _applicationContext.SaveChanges();
            return true;
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

        public List<Appartment> GetPersonsAppartments(int personId)
        {
            var existingPerson = _applicationContext.Persons.Find(personId);
            if (existingPerson == null)
                throw new ArgumentNullException(nameof(existingPerson));

            existingPerson.AppartmentPersonList = _applicationContext.
                AppartmentPersons.Where(x => x.PersonId == existingPerson.Id).ToList();
            if (existingPerson.AppartmentPersonList == null)
                return null;

            List<Appartment> personsAppartments = new List<Appartment>();

            foreach (var appartmentPerson in existingPerson.AppartmentPersonList)
            {

                personsAppartments.Add(
                    _applicationContext.Appartments.Find(appartmentPerson.AppartmentId)               
                    ) ;
            }

            return personsAppartments;
        }

        public int Insert(Person entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _applicationContext.Persons.Add(entity);
            _applicationContext.SaveChanges();

            return entity.Id;
        }

        public bool RemoveAppartmentsFromPerson(int personId, IEnumerable<Appartment> appartments)
        {
            if (appartments == null)
                throw new ArgumentNullException(nameof(appartments));

            var existingPerson = _applicationContext.Persons.Find(personId);
            if (existingPerson == null)
                throw new ArgumentNullException(nameof(existingPerson));

            foreach (var appartment in appartments)
            {
                var existingAppartment = _applicationContext.
                    Appartments.Find(appartment.Id);
                if (existingAppartment == null)
                    continue;

                var existingAppartmentPerson = _applicationContext.AppartmentPersons.
                    FirstOrDefault(x =>
                    x.AppartmentId == existingAppartment.Id &&
                    x.PersonId == existingAppartment.Id);

                if (existingAppartmentPerson != null)      
                    _applicationContext.Remove(existingAppartmentPerson);              
            }
            _applicationContext.SaveChanges();
            return true;
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
