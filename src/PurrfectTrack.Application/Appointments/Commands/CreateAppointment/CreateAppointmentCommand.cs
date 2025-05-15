using FluentValidation;
using PurrfectTrack.Domain.Enums;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Appointments.Commands.CreateAppointment;

public record CreateAppointmentCommand(string Title, string Description, DateTime StartDate, DateTime EndDate, string Location,
    Guid PetOwnerId, Guid PetId, Guid VetId, Guid? VetStaffId, AppointmentStatus Status, string Notes, Guid CompanyId) 
    : ICommand<CreateAppointmentResult>;

public record CreateAppointmentResult(Guid Id);

public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand>
{
    public CreateAppointmentValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required");

        RuleFor(x => x.StartDate)
            .NotEmpty().WithMessage("StartDate is required");

        RuleFor(x => x.EndDate)
            .NotEmpty().WithMessage("EndDate is required");

        RuleFor(x => x.PetOwnerId)
            .NotEmpty().WithMessage("PetOwnerId is required");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required");
    }
}
