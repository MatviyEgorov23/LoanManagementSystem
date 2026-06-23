using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using LoanManagementSystem.Models;

namespace LoanManagementSystem.Pages.Clients
{
    public class EditModel : PageModel
    {
        private readonly string _connectionString = "Server=DESKTOP-JH1JF57\\MSSQLSERVER01;Database=LoanDb;Trusted_Connection=True;TrustServerCertificate=True;";

        [BindProperty]
        public Client ClientData { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "SELECT * FROM Clients WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@id", id);
                conn.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        ClientData.Id = reader.GetInt32(0);
                        ClientData.Name = reader.GetString(1);
                        ClientData.Position = reader.GetString(2);
                        ClientData.Office = reader.GetString(3);
                        ClientData.Age = reader.GetInt32(4);
                    }
                    else
                    {
                        return RedirectToPage("/Clients/AddNewClient");
                    }
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                string sql = "UPDATE Clients SET Name=@name, Position=@pos, Office=@off, Age=@age WHERE Id=@id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@name", ClientData.Name);
                cmd.Parameters.AddWithValue("@pos", ClientData.Position);
                cmd.Parameters.AddWithValue("@off", ClientData.Office);
                cmd.Parameters.AddWithValue("@age", ClientData.Age);
                cmd.Parameters.AddWithValue("@id", ClientData.Id);

                conn.Open();
                cmd.ExecuteNonQuery();
            }

            return RedirectToPage("/Clients/AddNewClient");
        }
    }
}
