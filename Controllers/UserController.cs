using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherForecastAPI.Models;
using WeatherForecastAPI.Repository;

namespace WeatherForecastAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private UserRepository _userRepository;
        private FavoriteLocationRepository _locationRepository;

        public UserController(
            UserRepository userRepository,
            FavoriteLocationRepository locationRepository
        )
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
        }

        [HttpPost(Name = "Add User")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult CreateUser([FromBody] User newUser)
        {
            var user = _userRepository.AddUser(newUser.Id, newUser.Username);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpPost("{userId}/locations")]
        [ProducesResponseType(200, Type = typeof(void))]
        public IActionResult CreateFavoriteLocation(string userId, [FromBody] FavoriteLocation newLocation)
        {
            UserHasLocation? userFavLocation = _locationRepository.AddFavoriteLocation(
                userId,
                newLocation.Name
            );

            return Ok();
        }

        [HttpGet(Name = "Get All Users")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<User>))]
        public IActionResult GetUsers()
        {
            ICollection<User> users = _userRepository.GetUsers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(users);
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult GetUser(string userId)
        {
            User? user = _userRepository.GetUser(userId);
            Console.WriteLine($"Model State, {ModelState}, user {user}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpGet("{userId}/locations")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FavoriteLocation>))]
        public IActionResult GetUserLocations(string userId)
        {
            ICollection<FavoriteLocation>? userLocations = _userRepository.GetUserLocations(userId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(userLocations);
        }

        [HttpDelete("{userId}/locations")]
        [ProducesResponseType(200, Type = typeof(UserHasLocation))]
        public IActionResult DeleteFavoriteLocation(string userId, [FromBody] FavoriteLocation deleteLocation)
        {
            UserHasLocation? deletedLocation = _locationRepository.DeleteFavoriteLocation(
                userId,
                deleteLocation.Name
            );

            if (deletedLocation is null)
            {
                return BadRequest(new { Message = "Location cannot be found" });
            }

            return Ok(deletedLocation);
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult DeleteUser(string userId)
        {
            User? deletedUser = _userRepository.RemoveUser(userId);

            if (deletedUser is not null)
            {
                return BadRequest(new { Message = "User Not Found" });
            }

            return Ok(deletedUser);
        }
    }
}
