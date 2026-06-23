using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using LoanManagementSystem.Models;

namespace LoanManagementSystem.Pages.Clients
{
    public class AddNewClientModel : PageModel
    {
        private readonly string _connectionString = "Server=DESKTOP-JH1JF57\\MSSQLSERVER01;Database=LoanDb;Trusted_Connection=True;TrustServerCertificate=True;";
        
        public List<Client> ClientsList = new();
        [BindProperty]
        public Client NewClient { get; set; } = new();
        public void OnGet()
        {
            LoadClients();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                // ¬‡ÁÎË‚ËÈ ÏÂÚÓ‰ (“≤À‹ » ƒÀﬂ ƒ≈ÃŒÕ—“–¿÷≤Ø):
                //string sql = "INSERT INTO Clients (Name) VALUES ('" + NewClient.Name + "')";
                string sql = "INSERT INTO Clients (Name, Position, Office, Age, StartDate) VALUES (@name, @pos, @off, @age, @date)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", NewClient.Name);
                cmd.Parameters.AddWithValue("@pos", NewClient.Position);
                cmd.Parameters.AddWithValue("@off", NewClient.Office);
                cmd.Parameters.AddWithValue("@age", NewClient.Age);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM Clients WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToPage();
        }

        private void LoadClients()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Clients ORDER BY Id DESC";
                SqlCommand cmd = new SqlCommand(sql, conn);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ClientsList.Add(new Client
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Position = reader.GetString(2),
                            Office = reader.GetString(3),
                            Age = reader.GetInt32(4),
                            StartDate = reader.GetDateTime(5)
                        });
                    }
                }
            }
        }
    }
}
