using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using LoanManagementSystem.Models;

namespace LoanManagementSystem.Pages.Transactions
{
    public class CreateModel : PageModel
    {
        private readonly string _connectionString = "Server=DESKTOP-JH1JF57\\MSSQLSERVER01;Database=LoanDb;Trusted_Connection=True;TrustServerCertificate=True;";

        [BindProperty]
        public Transaction NewTransaction { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int LoanId { get; set; }

        public void OnGet(int loanId)
        {
            LoanId = loanId;
        }
        public IActionResult OnPost()
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var sqlTransit = conn.BeginTransaction())
                {
                    try
                    {
                       
                        string sqlInsert = "INSERT INTO Transactions (LoanId, Amount, PaymentMethod, Note) VALUES (@lid, @amt, @meth, @note)";

                        SqlCommand cmdInsert = new SqlCommand(sqlInsert, conn, sqlTransit);
                        cmdInsert.Parameters.AddWithValue("@lid", NewTransaction.LoanId);
                        cmdInsert.Parameters.AddWithValue("@amt", NewTransaction.Amount);
                        cmdInsert.Parameters.AddWithValue("@meth", NewTransaction.PaymentMethod);
                        cmdInsert.Parameters.AddWithValue("@note", NewTransaction.Note ?? "");

                        cmdInsert.ExecuteNonQuery();

                        string sqlUpdate = @"
                    UPDATE Loans 
                    SET Amount = Amount - @amt,
                        Status = CASE 
                                    WHEN (Amount - @amt) <= 0 THEN 'Closed' 
                                    ELSE Status 
                                 END
                    WHERE Id = @lid";

                        SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, conn, sqlTransit);

                        cmdUpdate.Parameters.AddWithValue("@amt", NewTransaction.Amount);
                        cmdUpdate.Parameters.AddWithValue("@lid", NewTransaction.LoanId);
                        cmdUpdate.ExecuteNonQuery();

                        sqlTransit.Commit();
                    }
                    catch
                    {
                        sqlTransit.Rollback();
                        throw;
                    }
                }
            }
            return RedirectToPage("/Loans/Index");
        }
    }
}