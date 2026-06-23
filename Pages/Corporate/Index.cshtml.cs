using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using LoanManagementSystem.Models;

namespace LoanManagementSystem.Pages.Corporate
{
    public class IndexModel : PageModel
    {
        private readonly string _connectionString = "Server=DESKTOP-JH1JF57\\MSSQLSERVER01;Database=LoanDb;Trusted_Connection=True;TrustServerCertificate=True;";
        public List<CorporateClient> Companies { get; set; } = new();
        public void OnGet()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id, CompanyName, Industry, City, NetProfit, RequestedAmountMax, Status FROM CorporateClients ORDER BY Id DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Companies.Add(new CorporateClient
                        {
                            Id = reader.GetInt32(0),
                            CompanyName = reader.GetString(1),
                            Industry = reader.GetString(2),
                            City = reader.GetString(3),
                            NetProfit = reader.GetDecimal(4),
                            RequestedAmountMax = reader.GetDecimal(5),
                            Status = reader.GetString(6)
                        });
                    }
                }
            }
        }
        public IActionResult OnPostDelete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM CorporateClients WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage();
        }
    }
}
