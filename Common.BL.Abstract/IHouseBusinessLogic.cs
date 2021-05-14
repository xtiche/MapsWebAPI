using Entity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Common.BL.Abstract
{
    public interface IHouseBusinessLogic
    {
        IEnumerable<Person> GetPeopleFromHouse(int houseId);
    }
}
