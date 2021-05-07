using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Abstract.Repositories
{
    public interface IHouseRepository : IBaseRepository<int, House>
    {
        bool AddAppartmentsToHouse(int houseId, IEnumerable<Appartment> appartments);
    }
}
