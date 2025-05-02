namespace PurrfectTrack.Application.Services;

public class AuditService : IAuditService
{
    public string GetCurrentUser()
    {
        // To be Implement logic to return the current user, e.g., from a session or token
        return "Administrator";

    }
}
