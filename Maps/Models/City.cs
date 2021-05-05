using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Maps.Models
{
    public class City : BaseEntity<int>
    {
        public string Name { get; set; }
        public List<Street> Streets { get; set; }

        public int? CountryId { get; set; }
        [JsonIgnore]
        public Country Country { get; set; }
    }
}
