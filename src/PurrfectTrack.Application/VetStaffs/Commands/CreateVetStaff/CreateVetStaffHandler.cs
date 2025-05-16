using Microsoft.EntityFrameworkCore;
using PurrfectTrack.Application.Data;
using PurrfectTrack.Application.Utils;
using PurrfectTrack.Domain.Enums;
using PurrfectTrack.Infrastructure.Caching;
using PurrfectTrack.Shared.CQRS;
using PurrfectTrack.Shared.Security;

namespace PurrfectTrack.Application.VetStaffs.Commands.CreateVetStaff;

public class CreateVetStaffHandler
    : BaseHandler, ICommandHandler<CreateVetStaffCommand, CreateVetStaffResult>
{
    private readonly ICacheService _cacheService;
    private readonly IPasswordHasher _passwordHasher;

    public CreateVetStaffHandler(IApplicationDbContext dbContext, ICacheService cacheService, IPasswordHasher passwordHasher)
        : base(dbContext)
    {
        _cacheService = cacheService;
        _passwordHasher = passwordHasher;
    }

    public async Task<CreateVetStaffResult> Handle(CreateVetStaffCommand command, CancellationToken cancellationToken)
    {
        var emailExists = await dbContext.Users
            .AnyAsync(u => u.Email == command.Email, cancellationToken);

        if (emailExists)
            throw new InvalidOperationException($"Email '{command.Email}' is already in use.");

        var hashedPassword = _passwordHasher.Hash(command.Password);

        var user = new User(
            command.Email,
            hashedPassword,
            UserRole.VetStaff
        );

        var vetStaff = new VetStaff(
            user.Id,
            command.FirstName,
            command.LastName,
            command.PhoneNumber,
            command.Address,
            command.DateOfBirth,
            command.Gender,
            command.Position,
            command.Department,
            command.EmploymentDate,
            command.CompanyId
        );

        var dbTransaction = await dbContext.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            dbContext.Users.Add(user);
            dbContext.VetStaffs.Add(vetStaff);

            await dbContext.SaveChangesAsync(cancellationToken);

            await dbTransaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await dbTransaction.RollbackAsync(cancellationToken);
            throw;
        }

        await _cacheService.RemoveAsync(CacheKeyManager.GetVetStaffsCacheKey());

        return new CreateVetStaffResult(vetStaff.Id);
    }
}