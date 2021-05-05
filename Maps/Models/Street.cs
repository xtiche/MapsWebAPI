using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maps.Models
{
    public class Street : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<House> Houses { get; set; }
    }
}
