using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using DataAccess.ReadModel;

namespace WebApplication.Controllers
{
    public class UsersController : ApiController
    {
        private readonly TestWorkshopEntities _db;

        public UsersController(TestWorkshopEntities dbEntities)
        {
            _db = dbEntities;
        }

        // GET: api/Users
        public IQueryable<UserDto> GetUsers()
        {
            return _db.Users.Select(users => new UserDto {Id = users.Id, Name = users.Name, Role = users.Role.ToString()});
        }

        // GET: api/Users/5
        [ResponseType(typeof(Users))]
        public IHttpActionResult GetUsers(Guid id)
        {
            Users users = _db.Users.Find(id);
            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // POST: api/Users
        [ResponseType(typeof(Users))]
        public IHttpActionResult PostUsers(string name, int role)
        {
            var newUser = new Users();
            newUser.Name = name;
            newUser.Role = role;
            newUser.Id = Guid.NewGuid();

            _db.Users.Add(newUser);
            _db.SaveChanges();
            return Ok();
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(Users))]
        public void DeleteUsers(Guid id)
        {
            Users users = _db.Users.Find(id);
            _db.Users.Remove(users);
            _db.SaveChanges();
        }
    }
}