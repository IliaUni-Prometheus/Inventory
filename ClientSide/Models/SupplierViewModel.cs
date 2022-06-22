﻿namespace ClientSide.Models
{
    public class SupplierViewModel
    {
        public int SupplierId { get; set; }
        public string CompanyName { get; set; } = null!;
        public string? ContactName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
        public string? HomePage { get; set; }
    }
}
