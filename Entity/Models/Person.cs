using Entity.Models.Abstract;
using System.Collections.Generic;

using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Entity.Models
{
    public class Person : BaseEntity<int>
    { 
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [JsonIgnore]
        public ICollection<AppartmentPerson> AppartmentPersonList { get; set; }

    }
}
