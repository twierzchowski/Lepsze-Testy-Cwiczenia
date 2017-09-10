namespace Application
{
    public interface ITiageBugService
    {
        int GetSeverity(string description);
        int GetPriority(string description);
    }
}