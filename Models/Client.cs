namespace LoanManagementSystem.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Position { get; set; } = "";
        public string Office { get; set; } = "";
        public int Age { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
    }
}
