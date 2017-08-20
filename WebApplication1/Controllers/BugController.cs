using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Mvc;
using Application;
using DataAccess;
using DataAccess.DTOs;
using Domain;

namespace WebApplication1.Controllers
{
    public class BugController : ApiController
    {
        private readonly ICommandHandler<CreateBugCommand> _service;
        private readonly IBugRepository _bugRepository;

        public BugController(ICommandHandler<CreateBugCommand> service, IBugRepository bugRepository)
        {
            _service = service;
            _bugRepository = bugRepository;
        }

        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("Bugs")]
        public List<BugDTO> Search()
        {
            return _bugRepository.GetActiveBugs().Select(bug => new BugDTO{Id = bug.Id, Title = bug.Title}).ToList();
        }

        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Bugs")]
        public void AddBug(CreateBugCommand command)
        {
            command.Id = Guid.NewGuid();
            _service.Handle(command);
        }

        //// GET: /Buggs/
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return Json(null, JsonRequestBehavior.AllowGet);
        //}


    }
}
