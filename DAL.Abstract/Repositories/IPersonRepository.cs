using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Abstract.Repositories
{
    interface IPersonRepository : IBaseRepository<int, Person>
    {
    }
}
