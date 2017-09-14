using System;

namespace Domain
{
    public interface IBugRepository
    {
        Bug GetById(Guid bugId);
        void Store(Bug bug);
    }
}