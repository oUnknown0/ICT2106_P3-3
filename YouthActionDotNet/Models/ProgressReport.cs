using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace YouthActionDotNet.Models
{
    public class ProgressReport
    {
        public ProgressReport()
        {
            this.reportId = Guid.NewGuid().ToString();
        }
        public string reportId { get; set; }

        public string reportName { get; set; }

        public string projectId { get; set; }

        public string reportDate { get; set; }
        
        [JsonIgnore]
        public virtual Project Project { get; set;}
    }
}