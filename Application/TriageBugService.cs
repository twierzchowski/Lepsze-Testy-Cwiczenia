using System.Net.Http;

namespace Application
{
    public class TriageBugService : ITriageBugService
    {
        private readonly string _baseUrl = "http://workshopaa.azurewebsites.net";

        public int GetSeverity(string title, string description)
        {
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/api/severity?title={title}&description={description}";
                return int.Parse(client.GetStringAsync(url).Result);
            }
        }

        public int GetPriority(string title, string description)
        {
            using (var client = new HttpClient())
            {
                string url = $"{_baseUrl}/api/priority?title={title}&description={description}";
                return int.Parse(client.GetStringAsync(url).Result);
            }
        }
    }
}