using System.Data.Entity;

namespace DataAccess
{
    public class BugsDbInitializer : DropCreateDatabaseIfModelChanges<BugManagementContext>
    {
        protected override void Seed(BugManagementContext bugManagementContext)
        {
            UserSeed.Seed(bugManagementContext);
            bugManagementContext.SaveChanges();
            BugSeed.Seed(bugManagementContext);
            bugManagementContext.SaveChanges();
        }
    }
}