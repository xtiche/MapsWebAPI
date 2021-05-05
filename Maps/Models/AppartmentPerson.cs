using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maps.Models
{
    public class AppartmentPerson
    {
        public int AppartmentId { get; set; }
        public Appartment Appartment { get; set; }

        public int PersonId { get; set; }
        public Person Person { get; set; }
    }
}
