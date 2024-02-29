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

        public User? GetUser(int id)
        {
            return _context.User.Where(u => u.Id == id).FirstOrDefault();
        }

        public User? GetUser(string username)
        {
            return _context.User.Where(u => u.Username == username).FirstOrDefault();
        }

        public ICollection<FavoriteLocation> GetUserLocations(int id)
        {
            return _context.UserHasLocations.Where(u => u.UserId == id).Select(u => u.FavoriteLocation).ToList();
        }

        public ICollection<FavoriteLocation> GetUserLocations(string username)
        {
            return _context.UserHasLocations.Where(u => u.User.Username == username).Select(u => u.FavoriteLocation).ToList();
        }

        public User AddUser(string username)
        {
            User user = new User{ Username = username };
            _context.User.Add(user);
            _context.SaveChanges();

            return user;
        }
    }
}
