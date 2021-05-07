using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Abstract.Repositories
{
    public interface IStreetRepository : IBaseRepository<int, Street>
    { 
        bool AddHousesToStreet(int streetId, IEnumerable<House> houses);
    }
}
