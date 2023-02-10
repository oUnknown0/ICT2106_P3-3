using System;
using Newtonsoft.Json;
namespace YouthActionDotNet.Models
{
    public class Expense
    {
        public Expense()
        {
            this.ExpenseId = Guid.NewGuid().ToString();
        }

        public string ExpenseId { get; set; }

        public string ExpenseAmount { get; set; }

        public string ExpenseDesc { get; set; }

        public string ProjectId { get; set; }

        public string ExpenseReceipt { get; set; }

        public string Status { get; set; }

        public DateTime DateOfExpense { get; set; }

        public DateTime DateOfSubmission { get; set; }

        public DateTime DateOfReimbursement { get; set; }

        public string ApprovalId { get; set; }

        [JsonIgnore]
        public virtual User user { get; set; }
        [JsonIgnore]
        public virtual Project project { get; set; }
        [JsonIgnore]
        public virtual File ExpenseReceiptFile { get; set; }
    }
}
