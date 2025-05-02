namespace PurrfectTrack.Domain;

public class DomainException : Exception
{
    public string ErrorCode { get; }

    public DomainException(string message, string errorCode = "DOMAIN_ERROR")
        : base($"Domain Exception: \"{message}\" thrown from Domain Layer.")
    {
        ErrorCode = errorCode;
    }

    public override string ToString()
    {
        return $"{base.ToString()}, Error Code: {ErrorCode}";
    }
}