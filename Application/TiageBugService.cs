using System.Net.Http;

namespace Application
{
    public class TiageBugService : ITiageBugService
    {
        private readonly TriageBugServiceProxy _serviceProxy;

        public TiageBugService(TriageBugServiceProxy serviceProxy)
        {
            _serviceProxy = serviceProxy;
        }
        public int GetSeverity(string description)
        {
            return _serviceProxy.GetSeverity(description);
        }

        public int GetPriority(string description)
        {
            return _serviceProxy.GetPriority(description);
        }
    }

    public class TriageBugServiceProxy
    {
        public int GetSeverity(string title)
        {
            using (var client = new HttpClient())
            {
                const string url = "http://localhost:55086/api/triagebug/severity";

                HttpResponseMessage response = client.PostAsJsonAsync(url, title).Result;
                response.EnsureSuccessStatusCode();

                var severity = response.Content.ReadAsAsync<int>().Result;
                return severity;
            }
        }

        public int GetPriority(string title)
        {
            using (var client = new HttpClient())
            {
                const string url = "http://localhost:55086/api/triagebug/priority";
                var urlWithParam = $"{url}?title={title}";

                HttpResponseMessage response = client.GetAsync(urlWithParam).Result;
                response.EnsureSuccessStatusCode();

                var priority = response.Content.ReadAsAsync<int>().Result;
                return priority;
            }
        }
    }
}