namespace PurrfectTrack.Domain.Abstractions;

public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }

    public void SetCreated(string user)
    {
        CreatedAt = DateTime.UtcNow;
        CreatedBy = user;
    }

    public void SetModified(string user)
    {
        LastModified = DateTime.UtcNow;
        LastModifiedBy = user;
    }
}