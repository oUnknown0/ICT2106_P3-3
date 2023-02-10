using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace YouthActionDotNet.Models
{
    public class Donor: User
    {
        // Can be organization or individual
        public string donorName { get; set; }

        // Either organization or individual
        public string donorType { get; set; }
        
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}