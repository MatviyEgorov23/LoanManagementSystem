namespace LoanManagementSystem.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = "";
        public decimal Amount { get; set; }
        public decimal InterestRate { get; set; }
        public int DurationMonths { get; set; }
        public DateTime StartDate { get; set; }
        public string Status { get; set; } = "Active";
    }
}
