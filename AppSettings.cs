using Microsoft.Extensions.Configuration;

namespace KnowledgeControl
{
    public class AppSettings : Services.Interfaces.IAppSettings
    {
        private readonly IConfiguration _config;

        public AppSettings(IConfiguration config)
        {
            _config = config;
        }

        public string KcConnectionString =>
            _config.GetConnectionString("KcConnectionString") ?? 
                throw new System.Exception("No db connection string");
    }
}