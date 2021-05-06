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
    class AppartmentRepository : IAppartmentRepository
    {
        private readonly ApplicationContext _applicationContext;
        public AppartmentRepository(ApplicationContext context)
        {
            _applicationContext = context;
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

        public int Insert(Appartment entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            _applicationContext.Appartments.Add(entity);
            _applicationContext.SaveChanges();

            return entity.Id;
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
