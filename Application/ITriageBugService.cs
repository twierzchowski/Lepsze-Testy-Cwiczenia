namespace Application
{
    public interface ITriageBugService
    {
        int GetSeverity(string title, string description);
        int GetPriority(string title, string description);
    }
}