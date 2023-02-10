using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace YouthActionDotNet.Models
{
    public class Project
    {
        public Project()
        {
            this.ProjectId = Guid.NewGuid().ToString();
        }
        public string ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }

        public string ProjectStartDate { get; set; }

        public string ProjectEndDate { get; set; }

        public string ProjectCompletionDate { get; set; }

        public string ProjectStatus { get; set; }

        public string ProjectType { get; set; }

        public double ProjectBudget { get; set; }

        public string ServiceCenterId {get; set;}
        
        [JsonIgnore]
        public virtual ServiceCenter ServiceCenter { get; set;}
    }
}