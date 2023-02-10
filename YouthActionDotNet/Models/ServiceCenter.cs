using System;
using Newtonsoft.Json;

namespace YouthActionDotNet.Models
{
    public class ServiceCenter
    {
        public ServiceCenter()
        {
            this.ServiceCenterId = Guid.NewGuid().ToString();
        }
        public string ServiceCenterId { get; set; }
        public string ServiceCenterName { get; set; }
        public string ServiceCenterAddress { get; set; }
        public string RegionalDirectorId { get; set; }

        [JsonIgnore]
        public virtual Employee RegionalDirector { get; set; }
    }
}