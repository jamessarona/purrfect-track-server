// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UpdateVetStaffHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

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