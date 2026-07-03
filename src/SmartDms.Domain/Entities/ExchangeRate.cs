namespace JuJuBi.Domain.Entities;

public class ExchangeRate
{
    public int Id { get; set; }
    public string CurrencyCode { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public decimal Ask { get; set; }        // តម្លៃលក់
    public decimal Bid { get; set; }        // តម្លៃទិញ
    public decimal Average { get; set; }
    public DateTime RateDate { get; set; }
    public string? Note { get; set; }
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}
