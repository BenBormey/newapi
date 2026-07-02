using System;

namespace JuJuBis.Domain.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; } = string.Empty;   // ឧ. USD, KHR
        public string CurrencyName { get; set; } = string.Empty;
        public decimal BuyRate { get; set; }
        public decimal SellRate { get; set; }
        public bool IsBase { get; set; }
        public bool Active { get; set; }
        public string? CurrencyNo { get; set; }
        public int? SupplierId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
