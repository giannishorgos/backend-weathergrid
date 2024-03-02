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
        private FavoriteLocationRepository _locationRepository;
        public UserController(UserRepository userRepository, FavoriteLocationRepository locationRepository)
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
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

        [HttpPost("{userId}/locations")]
        [ProducesResponseType(200, Type = typeof(void))]
        public IActionResult CreateFavoriteLocation(int userId, [FromQuery] string locationName)
        {
            UserHasLocation? userFavLocation = _locationRepository.AddFavoriteLocation(userId, locationName);

            return Ok();
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

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult GetUser(int id)
        {
            User? user = _userRepository.GetUser(id);


            Console.WriteLine($"Model State, {ModelState}");
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(user);
        }

        [HttpGet("{userId}/locations")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<FavoriteLocation>))]
        public IActionResult GetUserLocations(int userId)
        {
            ICollection<FavoriteLocation>? userLocations = _userRepository.GetUserLocations(userId);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(userLocations);
        }


        [HttpDelete("{userId}/location")]
        [ProducesResponseType(200, Type = typeof(UserHasLocation))]
        public IActionResult DeleteFavoriteLocation(int userId, [FromQuery] string locationName)
        {
            UserHasLocation? deletedLocation = _locationRepository.DeleteFavoriteLocation(userId, locationName);

            if(deletedLocation is null) 
            {
                return BadRequest(new { Message = "Location cannot be found" });
            }

            return Ok(new 
                { 
                    Message = $"Location { deletedLocation.FavoriteLocation.Name } deleted for user { deletedLocation.UserId }" 
                });
        }

        [HttpDelete("{userId}")]
        [ProducesResponseType(200, Type = typeof(User))]
        public IActionResult DeleteUser(int userId)
        {
            User? deletedUser = _userRepository.RemoveUser(userId);
            
            if(deletedUser is not null)
            {
                return BadRequest(new { Message = "User Not Found" });
            }

            return Ok(deletedUser);
        }
    }
}
