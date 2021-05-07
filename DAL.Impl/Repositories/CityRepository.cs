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
    public class CityRepository : ICityRepository
    {
        private readonly ApplicationContext _applicationContext;

        public CityRepository(ApplicationContext context)
        {
            _applicationContext = context;
        }

        public bool AddStreetsToCity(int cityId, IEnumerable<Street> streets)
        {
            if (streets == null)
                throw new ArgumentNullException(nameof(streets));

            var existingCity = _applicationContext.Cities.Find(cityId);
            if (existingCity == null)
                throw new ArgumentNullException(nameof(existingCity));

            foreach (var street in streets)
            {
                var existingStreet = _applicationContext.Streets.Find(street.Id);
                if (existingStreet == null)
                    throw new ArgumentNullException(nameof(existingStreet));

                existingStreet.CityId = existingCity.Id;

                _applicationContext.Update(existingStreet);
            }
            _applicationContext.SaveChanges();
            return true;
        }

        bool IBaseRepository<int, City>.Delete(int id)
        {
            City existingCity = _applicationContext.Cities.Find(id);

            if (existingCity == null)
                throw new ArgumentNullException(nameof(existingCity));

            _applicationContext.Remove(existingCity);
            _applicationContext.SaveChanges();

            return true;
        }

        IList<City> IBaseRepository<int, City>.GetAll()
        {
            return _applicationContext.Cities
                .Include(x => x.Streets)
                .ToList();
        }

        City IBaseRepository<int, City>.GetById(int id)
        {
            return _applicationContext.Cities.Find(id);
        }

        int IBaseRepository<int, City>.Insert(City entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _applicationContext.Cities.Add(entity);
            _applicationContext.SaveChanges();

            return entity.Id;
        }

        bool IBaseRepository<int, City>.Update(City entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            if (!_applicationContext.Cities.Any(c => c.Id == entity.Id))
                throw new ArgumentNullException(nameof(entity));

            _applicationContext.Update(entity);
            _applicationContext.SaveChanges();

            return true;
        }
    }
}
