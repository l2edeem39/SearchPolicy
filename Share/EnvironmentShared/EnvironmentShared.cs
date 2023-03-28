using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace SearchPolicy.Share.EnvironmentShared
{
    public static class EnvironmentShared
    {
        public static IHostingEnvironment _hostingEnvironment;
        public static IHttpContextAccessor _httpContextAccessor;
        public static IConfiguration _configuration;
        public static string GetConnectionString() => _configuration != null ? _configuration.GetConnectionString("corewsdb") : null;
        public static string GetConnectionString(string sectionName)
        {
            return _configuration.GetConnectionString(sectionName);
        }
        public static string GetProjectName()
        {
            return _configuration.GetSection("ProjectName").Value;
        }
        public static string GetProjectName(string sectionName)
        {
            return _configuration.GetSection(sectionName)?.Value;
        }
        public static string GetSection(string sectionName)
        {
            return _configuration.GetSection(sectionName)?.Value;
        }
    }
}
