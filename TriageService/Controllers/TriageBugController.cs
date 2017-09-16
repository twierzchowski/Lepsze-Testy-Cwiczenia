using System;
using System.Threading;
using System.Web.Http;

namespace TriageService.Controllers
{
    [RoutePrefix("api")]
    public class TriageBugController : ApiController
    {
        [HttpGet]
        [Route("severity")]
        public int GetSeverity(string title, string description)
        {
            Thread.Sleep(new Random().Next(500));
            if (description.Contains("fatal"))
            {
                // aaa! fatal bug
                return -1;
            }
            if (title.Length > 8 || description.Length > 100)
            {
                // this is very severe bug since it has long title/description 
                Thread.Sleep(new Random().Next(5000));
                return new Random().Next(99);
            }
            
            // otherwise normal bug
            return new Random().Next(100,250);
        }

        [HttpGet]
        [Route("priority")]
        public int GetPriority(string title, string description)
        {
            switch (DateTime.Now.Second %6)
            {
                case 1:
                    return 1;
                case 2:
                    return 2;
                case 0:
                    throw new Exception("Unknown exception");
                case 6:
                    return 6;
                default:
                    return 3;
            }
        }
    }
}
