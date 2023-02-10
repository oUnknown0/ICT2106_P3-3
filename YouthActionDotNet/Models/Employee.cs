using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace YouthActionDotNet.Models
{
    public class Employee: User
    {
        public string EmployeeNationalId { get; set; }

        public string BankName { get; set; }

        public string BankAccountNumber { get; set; }

        public string PAYE { get; set; }

        public string DateJoined { get; set; }

        public string EmployeeType { get; set; }

        public string EmployeeRole { get; set; }
        
        [JsonIgnore]
        public virtual User User { get; set; }
    }
}