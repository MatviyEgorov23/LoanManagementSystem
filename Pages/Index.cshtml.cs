using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;

namespace LoanManagementSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        private readonly string _connectionString = "Server=DESKTOP-JH1JF57\\MSSQLSERVER01;Database=LoanDb;Trusted_Connection=True;TrustServerCertificate=True;";


        public int TotalClients { get; set; }
        public int TotalCorporateClients { get; set; }
        public int TotalLoans { get; set; }
        public int ActiveLoans { get; set; }

        public void OnGet()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                TotalClients = GetCount(conn, "SELECT COUNT(*) FROM Clients");

                TotalCorporateClients = GetCount(conn, "SELECT COUNT(*) FROM CorporateClients");

                TotalLoans = GetCount(conn, "SELECT COUNT(*) FROM Loans");

                ActiveLoans = GetCount(conn, "SELECT COUNT(*) FROM Loans WHERE Status = 'Active'");
            }
        }

        private int GetCount(SqlConnection conn, string sql)
        {
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                return (int)cmd.ExecuteScalar();
            }
        }
    }
}
