using Entity.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Person : BaseEntity<int>
    { 
        public string Name { get; set; }
        public ICollection<AppartmentPerson> Appartments { get; set; }

    }
}
