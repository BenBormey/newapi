namespace JuJuBis.Domain.Entities;

/// <summary>Entity ស្នូលរបស់ DMS - គ្មាន dependency លើ framework ណាទេ</summary>
public class Document
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string DocumentType { get; set; } = string.Empty;   // ឧ. INVOICE, CONTRACT, REPORT
    public string? Description { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public bool IsArchived { get; set; }
}
