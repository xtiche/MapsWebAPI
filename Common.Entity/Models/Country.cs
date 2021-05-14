using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Entity.Models.Abstract;


namespace Entity.Models
{
        public class Country : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<City> Cities { get; set; }      
    }
}
