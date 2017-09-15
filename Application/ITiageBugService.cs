namespace Application
{
    public interface ITiageBugService
    {
        int GetSeverity(string title, string description);
        int GetPriority(string title, string description);
    }
}