namespace Template.Core.Share;

public interface IDemoCore : ICore<DemoPo, Guid>
{
    IEnumerable<DemoPo> GetHello();
}