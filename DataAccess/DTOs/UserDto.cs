using System;

namespace WebApplication.Controllers
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}