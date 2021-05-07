using DAL.Abstract.Repositories;
using Database;
using Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Impl.Repositories
{
    public class CountryRepository : ICountryRepository
    {
        private readonly ApplicationContext _applicationContext;

        public CountryRepository(ApplicationContext context)
        {
            _applicationContext = context;
        }

        public bool AddCitiesToCountry(int countryId, IEnumerable<City> cities)
        {
            if (cities == null)
                throw new ArgumentNullException(nameof(cities));

            var existingCountry = _applicationContext.Countries.Find(countryId);
            if (existingCountry == null)
                throw new ArgumentNullException(nameof(existingCountry));

            foreach(var city in cities)
            {
                var existingCity = _applicationContext.Cities.Find(city.Id);
                if (existingCity == null)
                    throw new ArgumentNullException(nameof(existingCity));

                existingCity.CountryId = existingCountry.Id;

                _applicationContext.Update(existingCity);
            }
            _applicationContext.SaveChanges();
            return true;
        }

        bool IBaseRepository<int, Country>.Delete(int id)
        {
            Country existingCountry = _applicationContext.Countries.Find(id);
            if (existingCountry == null)
                throw new ArgumentNullException(nameof(existingCountry));

            _applicationContext.Remove(existingCountry);
            _applicationContext.SaveChanges();

            return true;
        }

        IList<Country> IBaseRepository<int, Country>.GetAll()
        {
            return _applicationContext.Countries
                .Include(x => x.Cities)
                .ToList();
        }

        Country IBaseRepository<int, Country>.GetById(int id)
        {
            return _applicationContext.Countries.Find(id);
        }

        int IBaseRepository<int, Country>.Insert(Country entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _applicationContext.Countries.Add(entity);
            _applicationContext.SaveChanges();

            return entity.Id;
        }

        bool IBaseRepository<int, Country>.Update(Country entity)
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
