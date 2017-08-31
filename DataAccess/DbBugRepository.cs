﻿using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace DataAccess
{
    public class DbBugRepository : IBugRepository
    {
        private readonly BugManagementContext _bugManagementContext;

        public DbBugRepository(BugManagementContext bugManagementContext)
        {
            _bugManagementContext = bugManagementContext;
        }
        public Bug GetById(Guid bugId)
        {
            return _bugManagementContext.Bugs
                .SingleOrDefault(b => b.Id == bugId);
        }

        public List<Bug> GetBugs()
        {
            return _bugManagementContext.Bugs.ToList();
        }

        public void Store(Bug bug)
        {
            _bugManagementContext.Bugs.Add(bug);
        }
    }
}