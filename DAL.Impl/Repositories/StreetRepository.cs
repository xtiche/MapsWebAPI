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
    class StreetRepository : IStreetRepository
    {
        private readonly ApplicationContext _applicationContext;

        public StreetRepository(ApplicationContext context)
        {
            _applicationContext = context;
        }
        public bool Delete(int id)
        {
            Street existingStreet = _applicationContext.Streets.Find(id);
            if (existingStreet == null)
                throw new ArgumentNullException(nameof(existingStreet));

            _applicationContext.Remove(existingStreet);
            _applicationContext.SaveChanges();

            return true;
        }

        public IList<Street> GetAll()
        {
            return _applicationContext.Streets
                .Include(x => x.Houses)
                .ToList();
        }

        public Street GetById(int id)
        {
            return _applicationContext.Streets.Find(id);
        }

        public int Insert(Street entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _applicationContext.Streets.Add(entity);
            _applicationContext.SaveChanges();

            return entity.Id;
        }

        public bool Update(Street entity)
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
