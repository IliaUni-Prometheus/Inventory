namespace ClientSide.Models
{
    public class EmployeeViewModel
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Country { get; set; }
        public string? PostalCode { get; set; }
    }
}
