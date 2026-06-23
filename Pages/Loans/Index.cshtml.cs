using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using LoanManagementSystem.Models;

namespace LoanManagementSystem.Pages.Loans
{
    public class IndexModel : PageModel
    {
        private readonly string _connectionString = "Server=DESKTOP-JH1JF57\\MSSQLSERVER01;Database=LoanDb;Trusted_Connection=True;TrustServerCertificate=True;";

        public List<Loan> LoansList { get; set; } = new();

        public void OnGet()
        {
            LoadData();
        }

        private void LoadData()
        {
            LoansList.Clear();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = @"SELECT l.Id, l.Amount, l.InterestRate, l.DurationMonths, l.StartDate, l.Status, c.Name 
                               FROM Loans l 
                               JOIN Clients c ON l.ClientId = c.Id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        LoansList.Add(new Loan
                        {
                            Id = reader.GetInt32(0),
                            Amount = reader.GetDecimal(1),
                            InterestRate = reader.GetDecimal(2),
                            DurationMonths = reader.GetInt32(3),
                            StartDate = reader.GetDateTime(4),
                            Status = reader.GetString(5),
                            ClientName = reader.GetString(6)
                        });
                    }
                }
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            // ÇÀÕÈÑÒ: Ò³ëüêè Admin
            var role = HttpContext.Session.GetString("DemoRole") ?? "Guest";
            if (role != "Admin") return Forbid();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Loans WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage();
        }

        public IActionResult OnPostUpdateStatus(int id, string currentStatus)
        {
            // ÇÀÕÈÑÒ: Ò³ëüêè Admin
            var role = HttpContext.Session.GetString("DemoRole") ?? "Guest";
            if (role != "Admin") return Forbid();
            string nextStatus = currentStatus switch
            {
                "Active" => "Overdue",
                "Overdue" => "Closed",
                _ => "Active"
            };

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Loans SET Status = @status WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@status", nextStatus);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToPage();
        }
    }
}