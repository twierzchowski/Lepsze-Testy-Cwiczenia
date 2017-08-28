using Domain;

namespace DataAccess
{
    class BugHistoryRepository : IBugHistoryRepository
    {
        private readonly BugManagementContext _bugManagementContext;

        public BugHistoryRepository(BugManagementContext bugManagementContext)
        {
            _bugManagementContext = bugManagementContext;
        }
        public void Store(BugHistory bugHistory)
        {
            _bugManagementContext.AuditLogs.Add(bugHistory);
        }
    }
}