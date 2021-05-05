using Entity.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Street : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<House> Houses { get; set; }
    }
}
