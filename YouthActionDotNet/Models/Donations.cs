using System;
using Newtonsoft.Json;

namespace YouthActionDotNet.Models{

    public class Donations {
        public Donations(){
            this.DonationsId = Guid.NewGuid().ToString();
        }

        public string DonationsId { get; set; }

        public string DonationType  { get; set; }

        public string DonationAmount { get; set; }

        public string DonationConstraint { get; set; } 

        public DateTime DonationDate { get; set; }

        public string DonorId { get; set; }

        public string ProjectId { get; set; }

        [JsonIgnore]
        public virtual Donor donor { get; set; }
        [JsonIgnore]
        public virtual Project project { get; set; }
    }
}