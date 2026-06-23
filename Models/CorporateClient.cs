using System.ComponentModel.DataAnnotations;

namespace LoanManagementSystem.Models
{
    public class CorporateClient
    {
        public int Id { get; set; }
        public string CompanyName { get; set; } = "";
        public string Industry { get; set; } = "";
        public string RegistrationNumber { get; set; } = "";
        public string City { get; set; } = "";
        public string ContactPhone { get; set; } = "";
        public string Email { get; set; } = "";

        public decimal AssetsValue { get; set; }
        public decimal AnnualTurnover { get; set; }
        public decimal NetProfit { get; set; }
        public int EmployeeCount { get; set; }

        public decimal RequestedAmountMin { get; set; }
        public decimal RequestedAmountMax { get; set; }
        public decimal ProposedInterestRate { get; set; }
        public string FinancialReportSummary { get; set; } = "";
        public string CEO_Name { get; set; } = "";

        public DateTime ApplicationDate { get; set; } = DateTime.Now;
        public string Status { get; set; } = "Pending";
    }
}
