using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WeatherForecastAPI.Models;
using WeatherForecastAPI.Repository;

namespace WeatherForecastAPI.Controllers
{
    /// <summary>
    /// Represents user controller. Needs to be authorized.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private UserRepository _userRepository;
        private FavoriteLocationRepository _locationRepository;

        /// <summary>
        /// Creates a new instance, injecting <see cref="UserRepository"/> and <see cref="FavoriteLocationRepository"/>.
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="locationRepository">The favorite location repository</param>
        public UserController(
            UserRepository userRepository,
            FavoriteLocationRepository locationRepository
        )
        {
            _userRepository = userRepository;
            _locationRepository = locationRepository;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="newUser">The new user</param>
        /// <returns>The new user</returns>
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

        /// <summary>
        /// Adds a favorite location to a user's list of favorite locations.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <param name="newLocation">The new location</param>
        /// <returns>The newly added user's favorite location</returns>
        [HttpPost("{userId}/locations")]
        [ProducesResponseType(200, Type = typeof(UserHasLocation))]
        public IActionResult CreateFavoriteLocation(
            string userId,
            [FromBody] FavoriteLocation newLocation
        )
        {
            UserHasLocation? userFavLocation = _locationRepository.AddFavoriteLocation(
                userId,
                newLocation.Name
            );

            return Ok(userFavLocation);
        }

        /// <summary>
        /// Gets all users in the database.
        /// </summary>
        /// <returns>ICollection containing users</returns>
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

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <returns>The user with the given ID</returns>
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

        /// <summary>
        /// Gets all favorite locations for a user.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <returns>ICollection containing favorite locations</returns>
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

        /// <summary>
        /// Deletes a favorite location from a user's list of favorite locations.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <param name="deleteLocation">The location to delete</param>
        /// <returns>The deleted user's favorite location</returns>
        [HttpDelete("{userId}/locations")]
        [ProducesResponseType(200, Type = typeof(UserHasLocation))]
        public IActionResult DeleteFavoriteLocation(
            string userId,
            [FromBody] FavoriteLocation deleteLocation
        )
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

        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <returns>The deleted user</returns>
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
