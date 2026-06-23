using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using LoanManagementSystem.Models;

namespace LoanManagementSystem.Pages.Reports
{
    public class AnalyticsModel : PageModel
    {
        private string connectionString = "Server=DESKTOP-JH1JF57\\MSSQLSERVER01;Database=LoanDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public LoanAnalyticsViewModel Stats { get; set; } = new();

        public IActionResult OnGet()
        {
            // Only for admin
            var role = HttpContext.Session.GetString("DemoRole") ?? "Guest";
            if (role != "Admin") return RedirectToPage("/Index");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                // Procedure name
                SqlCommand cmd = new SqlCommand("GetLoanAnalytics", conn);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Stats.TotalActiveLoans = reader.GetInt32(0);
                        Stats.TotalLoanedAmount = reader.IsDBNull(1) ? 0 : reader.GetDecimal(1);
                        Stats.AverageRate = reader.IsDBNull(2) ? 0 : reader.GetDecimal(2);
                        Stats.OverdueCount = reader.GetInt32(3);
                    }
                }
            }
            return Page();
        }
    }
}