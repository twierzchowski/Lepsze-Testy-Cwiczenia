using System.Net.Http;

namespace Application
{
    public class TiageBugService : ITiageBugService
    {
        public int GetSeverity(string title, string description)
        {
            using (var client = new HttpClient())
            {
                string url = "http://workshopaa.azurewebsites.net/api/severity";
                var urlWithParam = $"{url}?title={title}&description={description}";
                return int.Parse(client.GetStringAsync(urlWithParam).Result);
            }
        }

        public int GetPriority(string title, string description)
        {
            using (var client = new HttpClient())
            {
                string url = "http://workshopaa.azurewebsites.net/api/priority";
                var urlWithParam = $"{url}?title={title}&description={description}";
                return int.Parse(client.GetStringAsync(urlWithParam).Result);
            }
        }
    }
}