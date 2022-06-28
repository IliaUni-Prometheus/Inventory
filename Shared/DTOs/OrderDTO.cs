namespace Shared.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? Freight { get; set; }
        public string? ShipAddress { get; set; }
        public string? ShipPostalCode { get; set; }
    }
}
