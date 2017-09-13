using System.Data.Entity;
using Domain;

namespace DataAccess
{
    public class BugManagementContext : DbContext
    {
        public DbSet<Bug> Bugs { get; set; }

        public DbSet<BugHistory> AuditLogs { get; set; }
        public BugManagementContext(string connectionString) : base(connectionString)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<BugManagementContext>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("BugManagement");
        }
    }
}
