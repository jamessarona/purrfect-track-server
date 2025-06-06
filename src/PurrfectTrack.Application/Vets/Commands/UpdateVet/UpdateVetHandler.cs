﻿// -----------------------------------------------------------------------------
//  Copyright © 2025 James Angelo
//  All rights reserved.
//
//  File:        UpdateVetHandler
//  Created:     5/17/2025 1:41:18 AM
//
//  This file is part of the PurrfectTrack.Server.
//  Unauthorized copying or distribution is prohibited.
// -----------------------------------------------------------------------------

namespace PurrfectTrack.Application.Vets.Commands.UpdateVet;

public class UpdateVetHandler
    : BaseHandler, ICommandHandler<UpdateVetCommand, UpdateVetResult>
{
    private readonly ICacheService _cacheService;

    public UpdateVetHandler(IApplicationDbContext dbContext, ICacheService cacheService) 
        : base(dbContext)
    {
        _cacheService = cacheService;
    }

    public async Task<UpdateVetResult> Handle(UpdateVetCommand command, CancellationToken cancellationToken)
    {
        var vet = await dbContext.Vets
            .FirstOrDefaultAsync(v => v.Id == command.Id, cancellationToken);

        if (vet is null)
            throw new VetNotFoundException(command.Id);

        vet.FirstName = command.FirstName;
        vet.LastName = command.LastName;
        vet.PhoneNumber = command.PhoneNumber ?? vet.PhoneNumber;
        vet.Address = command.Address ?? vet.Address;
        vet.DateOfBirth = command.DateOfBirth ?? vet.DateOfBirth;
        vet.Gender = command.Gender ?? vet.Gender;
        vet.LicenseNumber = command.LicenseNumber ?? vet.LicenseNumber;
        vet.LicenseExpiryDate = command.LicenseExpiryDate ?? vet.LicenseExpiryDate;
        vet.Specialization = command.Specialization ?? vet.Specialization;
        vet.YearsOfExperience = command.YearsOfExperience ?? vet.YearsOfExperience;
        vet.ClinicName = command.ClinicName ?? vet.ClinicName;
        vet.ClinicAddress = command.ClinicAddress ?? vet.ClinicAddress;
        vet.EmploymentDate = command.EmploymentDate ?? vet.EmploymentDate;
        vet.CompanyId = command.CompanyId ?? vet.CompanyId;

        await dbContext.SaveChangesAsync(cancellationToken);

        await _cacheService.RemoveAsync(CacheKeyManager.GetVetsCacheKey());
        await _cacheService.RemoveAsync(CacheKeyManager.GetVetByIdCacheKey(vet.Id));

        return new UpdateVetResult(vet.Id);
    }
}