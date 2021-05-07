using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Abstract.Repositories
{
    public interface ICityRepository : IBaseRepository<int, City>
    {
        bool AddStreetsToCity(int cityId, IEnumerable<Street> streets);
    }
}
