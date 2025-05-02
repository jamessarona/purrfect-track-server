namespace PurrfectTrack.Application.Services;

public class AuditService : IAuditService
{
    public string GetCurrentUser()
    {
        // To beImplement logic to return the current user, e.g., from a session or token
        return "Administrator";

    }
}
