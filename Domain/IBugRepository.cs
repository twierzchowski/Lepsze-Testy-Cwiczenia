using System;

namespace Domain
{
    public interface IBugRepository
    {
        Bug GetById(Guid id);
        void Store(Bug bug);
    }
}