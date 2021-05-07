using Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Abstract.Repositories
{
    public interface ICountryRepository : IBaseRepository<int, Country>
    {
        bool AddCitiesToCountry(int countryId, IEnumerable<City> cities);
    }
}
