using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Abstract.Repositories
{
    public interface IAppartmentRepository : IBaseRepository<int, Appartment>
    {
        bool AddPeopleToAppartment(int appartmentId, IEnumerable<Person> people);
    }
}
