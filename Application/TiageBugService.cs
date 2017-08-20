using System;
using System.Net.Http;
using System.Text;

namespace Application
{
    public class TiageBugService : ITiageBugService
    {
        private readonly TriageBugServiceProxy _serviceProxy;

        public TiageBugService(TriageBugServiceProxy serviceProxy)
        {
            _serviceProxy = serviceProxy;
        }
        public int GetTriage(string description)
        {
            return _serviceProxy.GetTriage(description);
        }
    }

    public class TriageBugServiceProxy
    {
        public TriageBugServiceProxy()
        {
            
        }
        public int GetTriage(string description)
        {
            using (var client = new HttpClient())
            {
                var content = new StringContent(description, Encoding.UTF8, "application/json");
                string url = "http://localhost:55086/api/triagebug";
                

                HttpResponseMessage response = client.PostAsJsonAsync(url, description).Result;
                response.EnsureSuccessStatusCode();

                var severity = response.Content.ReadAsAsync<int>().Result;
                return severity;
            }
        }
    }
}