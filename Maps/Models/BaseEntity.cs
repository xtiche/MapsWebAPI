using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Maps.Models
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
