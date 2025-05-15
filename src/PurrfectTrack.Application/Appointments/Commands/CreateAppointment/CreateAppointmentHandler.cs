using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.Appointments.Commands.CreateAppointment;

public class CreateAppointmentHandler
    : BaseHandler, ICommandHandler<CreateAppointmentCommand, CreateAppointmentResult>
{
    public CreateAppointmentHandler(IApplicationDbContext dbContext) 
        : base(dbContext) { }

    public async Task<CreateAppointmentResult> Handle(CreateAppointmentCommand command, CancellationToken cancellationToken)
    {
        var appointment = new Appointment(
            command.Title,
            command.Description,
            command.StartDate,
            command.EndDate,
            command.Location,
            command.PetOwnerId,
            command.PetId,
            command.VetId,
            command.VetStaffId,
            command.Status,
            command.Notes,
            command.CompanyId
            );

        dbContext.Appointments.Add(appointment);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateAppointmentResult(appointment.Id);
    }
}
