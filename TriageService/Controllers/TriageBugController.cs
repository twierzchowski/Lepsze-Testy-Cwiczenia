using System;
using System.Threading;
using System.Web.Http;

namespace TriageService.Controllers
{
    public class TriageBugController : ApiController
    {
        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/TriageBug
        public int Post([FromBody]string title)
        {
            if (title.Length > 10)
            {
                Thread.Sleep(new Random().Next(10000));
                return 2;
            }
            return 3;
        }
    }

    public class BugDTO
    {
        public string Title { get; set; }
    }
}
