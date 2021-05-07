using ADO.DAL.Impl.Infrastructure;
using DAL.Abstract.Repositories;
using Entity.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ADO.DAL.Impl.Repositories
{
    public class CountryRepository : BaseRepository<int, Country>, ICountryRepository
    {
        public CountryRepository(SqlConnection connection, SqlTransactionManager transactionManager)
            : base(connection, transactionManager)
        { }
        public bool AddCitiesToCountry(int countryId, IEnumerable<City> cities)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IList<Country> GetAll()
        {
            return base.ExecuteSelect("Select c.Id, c.Name from Countries c");
        }

        public Country GetById(int id)
        {
            throw new NotImplementedException();
        }

        public override int Insert(Country entity)
        {
            throw new NotImplementedException();
        }

        public override bool Update(Country entity)
        {
            throw new NotImplementedException();
        }

        public override Country DefaultRowMapping(SqlDataReader reader)
        {
            return new Country
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Name"]
            };
        }
    }
}
