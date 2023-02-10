using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace YouthActionDotNet.Models
{
    public class Volunteer: User
    {   

        public string VolunteerNationalId { get; set; }

        public string VolunteerDateJoined { get; set; }

        public string VolunteerDateBirth { get; set; }

        public string Qualifications { get; set; }

        public string CriminalHistory { get; set; }

        public string CriminalHistoryDesc { get; set; }

        public string ApprovalStatus { get; set; }

        public string ApprovedBy { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
    }
}