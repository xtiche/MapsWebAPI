using Entity.Models.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class House : BaseEntity<int>
    {
        public string Number { get; set; }
        public decimal PosX { get; set; }
        public decimal PosY { get; set; }
        public List<Appartment> Appartments { get; set; }

        public int? StreetId { get; set; }
        [JsonIgnore]
        public House Street { get; set; }
    }
}
