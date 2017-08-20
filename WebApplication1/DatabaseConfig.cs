using System.Configuration;
using System.Data.Entity;
using DataAccess;

namespace WebApplication1
{
    public class DatabaseConfig
    {
        public static void Setup()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            using (var dbContext = new BugManagementContext(connectionString))

            {
                Database.SetInitializer(new BugsDbInitializer());
                dbContext.Database.Initialize(true);
            }
        }

    }
}