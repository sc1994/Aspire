namespace Template.Entity;

public interface ICreated
{
    DateTime CreatedAt { get; set; }

    string CreatedBy { get; set; }
}