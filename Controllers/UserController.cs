using Microsoft.AspNetCore.Mvc;
using WeatherForecastAPI.Repository;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private UserRepository _userRepository;
        public UserController(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost(Name = "Add User")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult CreateUser([FromQuery] string Username)
        {
            var user = _userRepository.AddUser(Username);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpGet(Name = "Get All Users")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            ICollection<User> users = _userRepository.GetUsers();

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet(Name = "Get User")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult GetUser([FromQuery] int id)
        {
            User? user = _userRepository.GetUser(id);

            if(!ModelState.IsValid)
            {
                Console.WriteLine($"Model State, {ModelState}");
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

    }
}
