using System;
using System.Threading;
using System.Web.Http;

namespace TriageService.Controllers
{
    [RoutePrefix("api/triagebug")]
    public class TriageBugController : ApiController
    {
        [HttpPost]
        [Route("Severity")]
        public int GetSeverity([FromBody]string title)
        {
            if (title.Length > 10)
            {
                Thread.Sleep(new Random().Next(10000));
                return 2;
            }
            return 3;
        }

        [HttpGet]
        [Route("Priority")]
        public int GetPriority([FromBody]string title)
        {
            switch (DateTime.Now.Second %3) //%6?
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    throw new Exception("Unknown exception");
                default:
                    return 3;
            }
        }
    }

    public class BugDTO
    {
        public string Title { get; set; }
    }
}
