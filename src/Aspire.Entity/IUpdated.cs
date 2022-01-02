namespace Aspire.Entity;

public interface IUpdated
{
    DateTime UpdatedAt { get; set; }

    string UpdatedBy { get; set; }
}