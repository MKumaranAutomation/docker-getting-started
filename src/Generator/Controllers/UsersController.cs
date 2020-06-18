using System.Collections.Generic;
using Bogus;
using Generator.Models;
using Microsoft.AspNetCore.Mvc;

namespace Generator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("users")]
        public ActionResult<IEnumerable<User>> GetUsers(int count)
        {
            var users = new Faker<User>()
                .RuleFor(x => x.Id, x => ++x.IndexVariable)
                .RuleFor(x => x.Name, x => x.Name.FullName())
                .RuleFor(x => x.Email, x => x.Internet.Email())
                .RuleFor(x => x.Phone, x => x.Phone.PhoneNumber())
                .Generate(count);

            return Ok(users);
        }
    }
}
