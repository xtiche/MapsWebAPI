using Maps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Maps.Repositories
{
    public interface ICountryRepository: IBaseRepository<int, Country>
    {
        bool AddCityToCounty(int countryId, int cityId);
    }
}
