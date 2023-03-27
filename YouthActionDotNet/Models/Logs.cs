using System;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace YouthActionDotNet.Models {
    public class Logs {
        public Logs() {
            this.logId = Guid.NewGuid().ToString();
        }

        public string logId {get; set;}
        public string logUserName {get; set;}
        public string logAction {get; set;}

        [JsonIgnore]
        public virtual Project project { get; set;}

        [JsonIgnore]
        public virtual User user {get; set;}
    }
}