using System.Data.Entity;

namespace DataAccess
{
    public class BugsDbInitializer : DropCreateDatabaseIfModelChanges<BugManagementContext>
    {
        protected override void Seed(BugManagementContext bugManagementContext)
        {
            ProritySeed.Seed(bugManagementContext);
            SeveritySeed.Seed(bugManagementContext);
            BugSeed.Seed(bugManagementContext);
        }
    }
}