using System;
using System.Collections.Generic;

namespace Domain
{
    public interface IBugRepository
    {
        Bug GetById(Guid bugId);
        void Store(Bug bug);
    }
}