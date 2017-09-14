using System;

namespace Domain
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public UserRole Role { get; private set; }

        private User()
        { }
        public User(string name, UserRole role)
        {
            Name = name;
            Role = role;
            Id = Guid.NewGuid();
        }
    }

    public enum UserRole
    {
        Dev,
        Qa
    }
}