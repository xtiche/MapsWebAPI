using Entity.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Appartment : BaseEntity<int>
    {
        public int Number { get; set; }
        [JsonIgnore]
        public ICollection<AppartmentPerson> AppartmentPersonList { get; set; }

        public int HouseId { get; set; }
        [JsonIgnore]
        public House House { get; set; }
    }
}
