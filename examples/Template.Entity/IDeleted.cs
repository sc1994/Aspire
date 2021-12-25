namespace Template.Entity;

public interface IDeleted
{
    bool IsDeleted { get; set; }

    DateTime DeletedAt { get; set; }

    string DeletedBy { get; set; }
}