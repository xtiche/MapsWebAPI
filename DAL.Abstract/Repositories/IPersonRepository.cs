using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Abstract.Repositories
{
    public interface IPersonRepository : IBaseRepository<int, Person>
    {
        bool AddAppartmentToPerson(int personId, IEnumerable<Appartment> appartments);
    }
}
