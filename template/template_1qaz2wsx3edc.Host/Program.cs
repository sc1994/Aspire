using Microsoft.Extensions.Hosting;
using MicrosoftHost = Microsoft.Extensions.Hosting.Host;
using Microsoft.Extensions.Hosting;

namespace template_1qaz2wsx3edc.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return MicrosoftHost.ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}