using WeatherForecastAPI.Data;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Repository
{
    public class UserRepository
    {
        private ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public ICollection<User> GetUsers()
        {
            return _context.User.OrderBy(u => u.Id).ToList();
        }

        public User? GetUser(string userId)
        {
            Console.WriteLine(userId);
            var user = _context.User.Find(userId);

            return user;
        }

        public ICollection<FavoriteLocation> GetUserLocations(string id)
        {
            return _context
                .UserHasLocations.Where(u => u.UserId == id)
                .Select(u => u.FavoriteLocation)
                .ToList();
        }

        public User AddUser(string userId, string username)
        {
            User user = new User { Id = userId, Username = username };
            _context.User.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User? RemoveUser(string id)
        {
            User? user = _context.User.Find(id);

            if (user is not null)
            {
                _context.User.Remove(user);
            }

            return user;
        }
    }
}
