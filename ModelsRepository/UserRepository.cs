using WeatherForecastAPI.Data;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Repository
{
    /// <summary>
    /// Repository for user data. Directly interacts with the database.
    /// </summary>
    public class UserRepository
    {
        private ApplicationDBContext _context;

        /// <summary>
        /// Creates a new instance, injecting <see cref="ApplicationDBContext"/>.
        /// </summary>
        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all users in the database.
        /// </summary>
        /// <returns>ICollection containing users</returns>
        public ICollection<User> GetUsers()
        {
            return _context.User.OrderBy(u => u.Id).ToList();
        }

        /// <summary>
        /// Gets a user by their ID.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <returns>The user with the given ID, or null if not found</returns>
        public User? GetUser(string userId)
        {
            Console.WriteLine(userId);
            var user = _context.User.Find(userId);

            return user;
        }

        /// <summary>
        /// Gets all favorite locations for a user.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <returns>ICollection containing favorite locations</returns>
        public ICollection<FavoriteLocation> GetUserLocations(string userId)
        {
            return _context
                .UserHasLocations.Where(u => u.UserId == userId)
                .Select(u => u.FavoriteLocation)
                .ToList();
        }

        /// <summary>
        /// Adds a user to the database.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <param name="username">The user's username</param>
        /// <returns>The user that was added</returns>
        public User AddUser(string userId, string username)
        {
            User user = new User { Id = userId, Username = username };
            _context.User.Add(user);
            _context.SaveChanges();

            return user;
        }

        /// <summary>
        /// Removes a user from the database.
        /// </summary>
        /// <param name="userId">The user's ID</param>
        /// <returns>The user that was removed, or null if not found</returns>
        public User? RemoveUser(string userId)
        {
            User? user = _context.User.Find(userId);

            if (user is not null)
            {
                _context.User.Remove(user);
            }

            return user;
        }
    }
}
