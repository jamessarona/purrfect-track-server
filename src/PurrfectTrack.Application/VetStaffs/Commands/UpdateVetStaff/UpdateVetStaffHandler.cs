using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Exceptions;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Infrastructure.Caching;
using PurrfectTrack.Shared.CQRS;

namespace PurrfectTrack.Application.VetStaffs.Commands.UpdateVetStaff;

public class UpdateVetStaffHandler
    : BaseHandler, ICommandHandler<UpdateVetStaffCommand, UpdateVetStaffResult>
{
    private readonly ICacheService _cacheService;

    public UpdateVetStaffHandler(IApplicationDbContext dbContext, ICacheService cacheService) 
        : base(dbContext)
    {
        _cacheService = cacheService;
    }

    public async Task<UpdateVetStaffResult> Handle(UpdateVetStaffCommand command, CancellationToken cancellationToken)
    {
        var vetStaff = await dbContext.VetStaffs
            .FirstOrDefaultAsync(v => v.Id == command.Id, cancellationToken);

        if (vetStaff is null)
            throw new VetStaffNotFoundException(command.Id);

        vetStaff.FirstName = command.FirstName;
        vetStaff.LastName = command.LastName;
        vetStaff.PhoneNumber = command.PhoneNumber ?? vetStaff.PhoneNumber;
        vetStaff.Address = command.Address ?? vetStaff.Address;
        vetStaff.DateOfBirth = command.DateOfBirth ?? vetStaff.DateOfBirth;
        vetStaff.Gender = command.Gender ?? vetStaff.Gender;
        vetStaff.Position = command.Position ?? vetStaff.Position;
        vetStaff.Department = command.Department ?? vetStaff.Department;
        vetStaff.EmploymentDate = command.EmploymentDate ?? vetStaff.EmploymentDate;
        vetStaff.CompanyId = command.CompanyId ?? vetStaff.CompanyId;

        await dbContext.SaveChangesAsync(cancellationToken);

        await _cacheService.RemoveAsync(CacheKeyManager.GetVetStaffsCacheKey());
        await _cacheService.RemoveAsync(CacheKeyManager.GetVetStaffByIdCacheKey(vetStaff.Id));

        return new UpdateVetStaffResult(vetStaff.Id);
    }
}