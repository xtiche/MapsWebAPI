using Entity.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Street : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<House> Houses { get; set; }

        public int? CityId { get; set; }
        [JsonIgnore]
        public City City { get; set; }
    }
}
