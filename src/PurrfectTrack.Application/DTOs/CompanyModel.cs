namespace PurrfectTrack.Application.DTOs;

public class CompanyModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Website { get; set; }
    public string? Address { get; set; }
    public string? TaxpayerId { get; set; }
}
