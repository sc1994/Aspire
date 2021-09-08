using Aspire;

namespace ServicesAndApplications.Applications;

public class DatetimeApplication : ApplicationBase
{
    public string Get()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }

    public string CreateOrUpdate()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
    }
}
