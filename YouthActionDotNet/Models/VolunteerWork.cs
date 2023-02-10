using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace YouthActionDotNet.Models
{
    public class VolunteerWork
    {
        VolunteerWork()
        {
            this.VolunteerWorkId = Guid.NewGuid().ToString();
        }
        public string VolunteerWorkId { get; set; }

        public string ShiftStart { get; set; }

        public string ShiftEnd { get; set; }

        public string SupervisingEmployee { get; set; }

        public string VolunteerId { get; set; }

        public string projectId { get; set; }
        
        [JsonIgnore]
        public virtual Employee employee { get; set; }
        [JsonIgnore]
        public virtual Volunteer volunteer { get; set; }
        [JsonIgnore]
        public virtual Project project { get; set; }
    }
}