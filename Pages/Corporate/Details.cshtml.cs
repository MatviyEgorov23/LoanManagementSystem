using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using LoanManagementSystem.Models;

namespace LoanManagementSystem.Pages.Corporate
{
    public class DetailsModel : PageModel
    {
        private readonly string _connectionString = "Server=DESKTOP-JH1JF57\\MSSQLSERVER01;Database=LoanDb;Trusted_Connection=True;TrustServerCertificate=True;";


        public CorporateClient Company { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {

                string sql = "SELECT * FROM CorporateClients WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Company.Id = reader.GetInt32(0);
                        Company.CompanyName = reader.GetString(1);
                        Company.Industry = reader.IsDBNull(2) ? "" : reader.GetString(2);
                        Company.RegistrationNumber = reader.IsDBNull(3) ? "" : reader.GetString(3);
                        Company.City = reader.IsDBNull(4) ? "" : reader.GetString(4);

                        Company.ContactPhone = reader.IsDBNull(6) ? "" : reader.GetString(6);
                        Company.Email = reader.IsDBNull(7) ? "" : reader.GetString(7);
                        Company.AssetsValue = reader.IsDBNull(8) ? 0 : reader.GetDecimal(8);
                        Company.AnnualTurnover = reader.IsDBNull(9) ? 0 : reader.GetDecimal(9);
                        Company.NetProfit = reader.IsDBNull(10) ? 0 : reader.GetDecimal(10);
                        Company.RequestedAmountMin = reader.IsDBNull(13) ? 0 : reader.GetDecimal(13);
                        Company.RequestedAmountMax = reader.IsDBNull(14) ? 0 : reader.GetDecimal(14);
                        Company.ProposedInterestRate = reader.IsDBNull(15) ? 0 : reader.GetDecimal(15);
                        Company.FinancialReportSummary = reader.IsDBNull(17) ? "" : reader.GetString(17);
                        Company.CEO_Name = reader.IsDBNull(19) ? "" : reader.GetString(19);
                        Company.Status = reader.IsDBNull(21) ? "" : reader.GetString(21);
                    }
                    else
                    {
                        return RedirectToPage("/Corporate/Index");
                    }
                }
            }
            return Page();
        }
    }
}
