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
    public class AppartmentRepository : IAppartmentRepository
    {
        private readonly ApplicationContext _applicationContext;
        public AppartmentRepository(ApplicationContext context)
        {
            _applicationContext = context;
        }

        public bool AddPeopleToAppartment(int appartmentId, IEnumerable<Person> people)
        {
            if (people == null)
                throw new ArgumentNullException(nameof(people));

            var existingAppartment = _applicationContext.Appartments.Find(appartmentId);
            if (existingAppartment == null)
                throw new ArgumentNullException(nameof(existingAppartment));

            foreach (var person in people)
            {
                var existingPerson = _applicationContext.Persons.Find(person.Id);
                if (existingPerson == null)
                    throw new ArgumentNullException(nameof(existingPerson));

                if (!_applicationContext.AppartmentPesrons.Any(x => x.AppartmentId == existingAppartment.Id && x.PersonId == existingPerson.Id)) {
                    _applicationContext.AppartmentPesrons.Add(
                        new AppartmentPerson {
                            AppartmentId = existingAppartment.Id,
                            PersonId = existingPerson.Id
                        });
                }
            }
            _applicationContext.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            Appartment existingAppartment = _applicationContext.Appartments.Find(id);
            if (existingAppartment == null)
                throw new ArgumentNullException(nameof(existingAppartment));

            _applicationContext.Remove(existingAppartment);
            _applicationContext.SaveChanges();

            return true;
        }

        public IList<Appartment> GetAll()
        {
            return _applicationContext.Appartments.ToList();
        }

        public Appartment GetById(int id)
        {
            return _applicationContext.Appartments.Find(id);
        }

        public List<Person> GetPeopleInAppartment(int appartmentId)
        {
            var existingAppartment = _applicationContext.Appartments.Find(appartmentId);
            if (existingAppartment == null)
                throw new ArgumentNullException(nameof(existingAppartment));

            List<Person> peopleInAppartment = new List<Person>();

            foreach (var appartmentPerson in existingAppartment.AppartmentPersonList)
            {
                peopleInAppartment.Add(appartmentPerson.Person);
            }

            return peopleInAppartment;
        }

        public int Insert(Appartment entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _applicationContext.Appartments.Add(entity);
            _applicationContext.SaveChanges();

            return entity.Id;
        }

        public bool RemovePeopleFromAppartment(int appartmentId, IEnumerable<Person> people)
        {
            if (people == null)
                throw new ArgumentNullException(nameof(people));

            var existingAppartment = _applicationContext.Appartments.Find(appartmentId);
            if (existingAppartment == null)
                throw new ArgumentNullException(nameof(existingAppartment));

            foreach (var person in people)
            {
                var existingPerson = _applicationContext.Persons.Find(person.Id);
                if (existingPerson == null)
                    continue;

                var existingAppartmentPerson = _applicationContext.AppartmentPesrons.
                    FirstOrDefault(x =>
                    x.AppartmentId == existingAppartment.Id &&
                    x.PersonId == existingAppartment.Id);

                if (existingAppartmentPerson != null)
                    _applicationContext.Remove(existingAppartmentPerson);
            }
            _applicationContext.SaveChanges();
            return true;
        }

        public bool Update(Appartment entity)
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
