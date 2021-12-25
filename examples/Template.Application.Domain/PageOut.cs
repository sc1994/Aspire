namespace Template.Application;

public class PageOut<T>
{
    public int Count { get; set; }

    public IEnumerable<T> List { get; set; } = Array.Empty<T>();
}