using System.ComponentModel.DataAnnotations;

namespace LoanManagementSystem.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int LoanId { get; set; } // До якого кредитного рахунку платіж привязаний

        [Required]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public string PaymentMethod { get; set; } = "Card"; // Card, Cash, Bank Transfer

        public string? Note { get; set; }
    }
}
