namespace ClientSide.Models
{
    public class InvoiceViewModel
    {
        public string? ShipName { get; set; }
        public string? ShipAddress { get; set; }
        public string CustomerName { get; set; }
        public string Salesperson { get; set; }
        public DateTime? OrderDate { get; set; }
    }
}
