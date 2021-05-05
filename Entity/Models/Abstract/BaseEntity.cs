using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Models.Abstract
{
    public abstract class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
    }
}
