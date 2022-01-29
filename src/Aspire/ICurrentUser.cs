namespace Aspire;

public interface ICurrentUser
{
    public string UserName { get; set; }

    public string TenementCode { get; set; }

    bool IsValidTenementCode { get; }
}