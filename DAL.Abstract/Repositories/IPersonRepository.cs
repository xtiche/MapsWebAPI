using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Abstract.Repositories
{
    public interface IPersonRepository : IBaseRepository<int, Person>
    {
        bool AddAppartmentsToPerson(int personId, IEnumerable<Appartment> appartments);
        bool RemoveAppartmentsFromPerson(int personId, IEnumerable<Appartment> appartments);
        List<Appartment> GetPersonsAppartments(int personId);
    }
}
