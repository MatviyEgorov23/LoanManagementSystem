namespace LoanManagementSystem.Models
{
    public class LoanAnalyticsViewModel
    {
        public int TotalActiveLoans { get; set; }
        public decimal TotalLoanedAmount { get; set; }
        public decimal AverageRate { get; set; }
        public int OverdueCount { get; set; }
    }
}