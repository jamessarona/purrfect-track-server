namespace PurrfectTrack.Application.Abstractions;

public interface IAuditService
{
    string GetCurrentUser(); // Method to get the current user, e.g., from HTTP context
}