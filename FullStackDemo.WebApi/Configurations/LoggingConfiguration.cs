using log4net.Config;
using log4net;
using System.Reflection;

namespace FullStackDemo.WebApi.Configurations
{
    public static class LoggingConfiguration
    {
        public static void ConfigureLogging(this WebApplicationBuilder builder)
        {
            var loggerRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(loggerRepository, new FileInfo("log4net.config"));
        }
    }
}
