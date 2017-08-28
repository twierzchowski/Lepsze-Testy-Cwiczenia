namespace Domain
{
    public interface IBugHistoryRepository
    {
        void Store(BugHistory bugHistory);
    }
}