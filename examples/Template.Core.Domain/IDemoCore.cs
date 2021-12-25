namespace Template.Core;

public interface IDemoCore : ICore<DemoDto, Guid>
{
    IEnumerable<DemoDto> GetHello();
}