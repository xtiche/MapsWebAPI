using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maps.Models
{
    public class Appartment : BaseEntity<int>
    {
        public string Number { get; set; }
        public ICollection<AppartmentPerson> Persons { get; set; }
    }
}
