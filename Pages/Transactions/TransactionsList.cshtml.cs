using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using LoanManagementSystem.Models;
using System.Collections.Generic;
using System;

namespace LoanManagementSystem.Pages.Transactions
{
    public class TransactionsListModel : PageModel
    {
        private string connectionString = "Server=DESKTOP-JH1JF57\\MSSQLSERVER01;Database=LoanDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public List<TransactionViewModel> Transactions { get; set; } = new List<TransactionViewModel>();

        [BindProperty(SupportsGet = true)]
        public DateTime? DateFrom { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? DateTo { get; set; }

        public IActionResult OnGet()
        {
            // CHECK SESSION: Only Admin can watch this page
            var role = HttpContext.Session.GetString("DemoRole") ?? "Guest";
            if (role != "Admin")
            {
                return RedirectToPage("/Index"); 
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // SQL query with JOIN for selecting clients names
                string sql = @"
                    SELECT 
                        T.Id, T.PaymentDate, T.Amount, T.PaymentMethod, T.Note,
                        ISNULL(C.Name, CC.CompanyName) AS BorrowerName,
                        L.Id AS LoanNumber
                    FROM Transactions T
                    JOIN Loans L ON T.LoanId = L.Id
                    LEFT JOIN Clients C ON L.ClientId = C.Id
                    LEFT JOIN CorporateClients CC ON L.ClientId = CC.Id
                    WHERE 1=1";

                // Date filters
                if (DateFrom.HasValue) sql += " AND T.PaymentDate >= @from";
                if (DateTo.HasValue) sql += " AND T.PaymentDate <= @to";

                sql += " ORDER BY T.PaymentDate DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                if (DateFrom.HasValue) cmd.Parameters.AddWithValue("@from", DateFrom.Value);
                if (DateTo.HasValue) cmd.Parameters.AddWithValue("@to", DateTo.Value);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transactions.Add(new TransactionViewModel
                        {
                            Id = reader.GetInt32(0),
                            Date = reader.GetDateTime(1),
                            Amount = reader.GetDecimal(2),
                            Method = reader.GetString(3),
                            Note = reader.IsDBNull(4) ? "" : reader.GetString(4),
                            Customer = reader.GetString(5),
                            LoanId = reader.GetInt32(6)
                        });
                    }
                }
            }
            return Page();
        }
        public class TransactionViewModel
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public decimal Amount { get; set; }
            public string Method { get; set; }
            public string Customer { get; set; }
            public int LoanId { get; set; }
            public string Note { get; set; }
        }
    }
}
