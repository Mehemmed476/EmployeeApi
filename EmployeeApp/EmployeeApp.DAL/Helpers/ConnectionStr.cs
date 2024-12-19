using Microsoft.Extensions.Configuration;

namespace EmployeeApp.DAL.Helpers;

public class ConnectionStr
{
    public static string GetConnectionString()
    {
        ConfigurationManager configurationManager = new ConfigurationManager();
        configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "EmployeeApp.API"));
        configurationManager.AddJsonFile("appsettings.json");
        
        string? connectionString = configurationManager.GetConnectionString("MacBookMsSql");
        if (connectionString is null)
        {
            throw new Exception("Connection String not found");
        }
        return connectionString;
    }
}