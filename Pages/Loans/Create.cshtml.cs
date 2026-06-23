using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using LoanManagementSystem.Models;

namespace LoanManagementSystem.Pages.Loans
{
    public class CreateModel : PageModel
    {
        private readonly string _connectionString = "Server=DESKTOP-JH1JF57\\MSSQLSERVER01;Database=LoanDb;Trusted_Connection=True;TrustServerCertificate=True;";

        [BindProperty]
        public Loan NewLoan { get; set; } = new();

        public List<SelectListItem> ClientOptions { get; set; } = new();
        public void OnGet()
        {
            LoadClients();
        }
        public IActionResult OnPost()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Loans (ClientId, Amount, InterestRate, DurationMonths, Status) 
                               VALUES (@clientId, @amount, @rate, @duration, 'Active')";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@clientId", NewLoan.ClientId);
                cmd.Parameters.AddWithValue("@amount", NewLoan.Amount);
                cmd.Parameters.AddWithValue("@rate", NewLoan.InterestRate);
                cmd.Parameters.AddWithValue("@duration", NewLoan.DurationMonths);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToPage("/Loans/Index");
        }

        private void LoadClients()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT Id, Name FROM Clients ORDER BY Name";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ClientOptions.Add(new SelectListItem
                        {
                            Value = reader.GetInt32(0).ToString(),
                            Text = reader.GetString(1)
                        });
                    }
                }
            }
        }
    }
}