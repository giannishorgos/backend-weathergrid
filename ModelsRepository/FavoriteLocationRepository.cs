using WeatherForecastAPI.Data;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Repository
{
    /// <summary>
    /// Repository for favorite location data. Directly interacts with the database.
    /// </summary>
    public class FavoriteLocationRepository
    {
        private ApplicationDBContext _context;
        private UserRepository _userRepository;

        /// <summary>
        /// Creates a new instance, injecting <see cref="ApplicationDBContext"/> and <see cref="UserRepository"/>.
        /// </summary>
        /// <param name="context">The database context</param>
        /// <param name="userRepository">The user repository</param>
        public FavoriteLocationRepository(
            ApplicationDBContext context,
            UserRepository userRepository
        )
        {
            _context = context;
            _userRepository = userRepository;
        }

        /// <summary>
        /// Adds a favorite location to a user's list of favorite locations.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <param name="locationName">The name of the location</param>
        /// <returns>The user's favorite location, or null if the user does not exists</returns>
        public UserHasLocation? AddFavoriteLocation(string userId, string locationName)
        {
            locationName = locationName.ToLower();

            User? user = _userRepository.GetUser(userId);
            UserHasLocation? userFavLocation = null;

            if (user is not null)
            {
                FavoriteLocation? location = _context
                    .FavoriteLocation.Where(loc => loc.Name == locationName)
                    .FirstOrDefault();

                if (location is null)
                {
                    location = new FavoriteLocation { Name = locationName };
                    _context.FavoriteLocation.Add(location);
                }

                userFavLocation = new UserHasLocation { User = user, FavoriteLocation = location };
                _context.UserHasLocations.Add(userFavLocation);
                _context.SaveChanges();
            }

            return userFavLocation;
        }

        /// <summary>
        /// Deletes a favorite location from a user's list of favorite locations.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <param name="locationName">The name of the location</param>
        /// <returns>The user's favorite location, or null if the user or location do not exist</returns>
        public UserHasLocation? DeleteFavoriteLocation(string userId, string locationName)
        {
            UserHasLocation? userFavLocation = _context
                .UserHasLocations.Where(userLocation =>
                    userLocation.UserId == userId
                    && userLocation.FavoriteLocation.Name == locationName
                )
                .FirstOrDefault();

            if (userFavLocation is not null)
            {
                _context.UserHasLocations.Remove(userFavLocation);
                _context.SaveChanges();
            }

            return userFavLocation;
        }
    }
}
