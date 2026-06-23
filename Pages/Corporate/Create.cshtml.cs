using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using LoanManagementSystem.Models;

namespace LoanManagementSystem.Pages.Corporate
{
    public class CreateModel : PageModel
    {
        private readonly string _connectionString = "Server=DESKTOP-JH1JF57\\MSSQLSERVER01;Database=LoanDb;Trusted_Connection=True;TrustServerCertificate=True;";

        [BindProperty]
        public CorporateClient Company { get; set; } = new();
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO CorporateClients 
                    (CompanyName, Industry, RegistrationNumber, City, ContactPhone, Email, 
                     AssetsValue, AnnualTurnover, NetProfit, EmployeeCount, 
                     RequestedAmountMin, RequestedAmountMax, ProposedInterestRate, 
                     FinancialReportSummary, CEO_Name, Status) 
                    VALUES 
                    (@name, @ind, @reg, @city, @phone, @email, 
                     @assets, @turnover, @profit, @employees, 
                     @min, @max, @rate, @report, @ceo, @status)";

                SqlCommand cmd = new SqlCommand(sql, conn);

                
                cmd.Parameters.AddWithValue("@name", Company.CompanyName ?? "");
                cmd.Parameters.AddWithValue("@ind", Company.Industry ?? "");
                cmd.Parameters.AddWithValue("@reg", Company.RegistrationNumber ?? "");
                cmd.Parameters.AddWithValue("@city", Company.City ?? "");
                cmd.Parameters.AddWithValue("@phone", Company.ContactPhone ?? "");
                cmd.Parameters.AddWithValue("@email", Company.Email ?? "");
                cmd.Parameters.AddWithValue("@assets", Company.AssetsValue);
                cmd.Parameters.AddWithValue("@turnover", Company.AnnualTurnover);
                cmd.Parameters.AddWithValue("@profit", Company.NetProfit);
                cmd.Parameters.AddWithValue("@employees", Company.EmployeeCount);
                cmd.Parameters.AddWithValue("@min", Company.RequestedAmountMin);
                cmd.Parameters.AddWithValue("@max", Company.RequestedAmountMax);
                cmd.Parameters.AddWithValue("@rate", Company.ProposedInterestRate);
                cmd.Parameters.AddWithValue("@report", Company.FinancialReportSummary ?? "");
                cmd.Parameters.AddWithValue("@ceo", Company.CEO_Name ?? "");
                cmd.Parameters.AddWithValue("@status", Company.Status ?? "Pending");

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToPage("/Corporate/Index");
        }
    }
}
