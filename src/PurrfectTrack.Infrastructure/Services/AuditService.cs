namespace PurrfectTrack.Infrastructure.Services;

public class AuditService : IAuditService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuditService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string GetCurrentUser()
    {
        return _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "SYSTEM";
    }
}