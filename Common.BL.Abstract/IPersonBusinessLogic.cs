using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.BL.Abstract
{
    public interface IPersonBusinessLogic
    {
        IEnumerable<Person> GetPeopleByLastName(string lastName);
    }
}
