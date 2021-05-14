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
    public class HouseRepository : IHouseRepository
    {
        private readonly ApplicationContext _applicationContext;

        public HouseRepository(ApplicationContext context)
        {
            _applicationContext = context;
        }

        public bool AddAppartmentsToHouse(int houseId, IEnumerable<Appartment> appartments)
        {
            if (appartments == null)
                throw new ArgumentNullException(nameof(appartments));

            var existingHouse = _applicationContext.Houses.Find(houseId);
            if (existingHouse == null)
                throw new ArgumentNullException(nameof(existingHouse));

            foreach (var appartment in appartments)
            {
                var existingAppartment = _applicationContext.Appartments.Find(appartment.Id);
                if (existingAppartment == null)
                    throw new ArgumentNullException(nameof(existingAppartment));

                existingAppartment.HouseId = existingHouse.Id;

                _applicationContext.Update(existingAppartment);
            }
            _applicationContext.SaveChanges();
            return true;
        }

        public bool RemoveAppartmentsFromHouse(int houseId, IEnumerable<Appartment> appartments)
        {
            if (appartments == null)
                throw new ArgumentNullException(nameof(appartments));

            var existingHouse = _applicationContext.Houses.Find(houseId);
            if (existingHouse == null)
                throw new ArgumentNullException(nameof(existingHouse));

            foreach (var appartment in appartments)
            {
                var existingAppartment = _applicationContext.Appartments.Find(appartment.Id);
                if (existingAppartment == null)
                    continue;

                existingAppartment.HouseId = 0;

                _applicationContext.Update(existingAppartment);
            }
            _applicationContext.SaveChanges();
            return true;
        }

        bool IBaseRepository<int, House>.Delete(int id)
        {
            House existingHouse = _applicationContext.Houses.Find(id);
            if (existingHouse == null)
                throw new ArgumentNullException(nameof(existingHouse));

            _applicationContext.Remove(existingHouse);
            _applicationContext.SaveChanges();

            return true;
        }

        IList<House> IBaseRepository<int, House>.GetAll()
        {
            return _applicationContext.Houses
                .Include(x => x.Appartments)
                .ToList();
        }

        House IBaseRepository<int, House>.GetById(int id)
        {
            return _applicationContext.Houses.Find(id);
        }

        int IBaseRepository<int, House>.Insert(House entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _applicationContext.Houses.Add(entity);
            _applicationContext.SaveChanges();

            return entity.Id;
        }

        bool IBaseRepository<int, House>.Update(House entity)
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
