using System;

namespace Domain
{
    public interface IUserRepository
    {
        User GetUser(Guid id);
    }
}