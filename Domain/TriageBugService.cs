using System.Configuration;
using System.Net.Http;

namespace Domain
{
        public class TriageBugService
        {
            private readonly string _baseUrl = ConfigurationManager.AppSettings["AutoTriageServiceUrl"];

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
