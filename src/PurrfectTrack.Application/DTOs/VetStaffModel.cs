namespace PurrfectTrack.Application.DTOs;

public class VetStaffModel : ContactModel
{
    public string? Position { get; set; }
    public string? Department { get; set; }
    public DateTime? EmploymentDate { get; set; }
}