using WeatherForecastAPI.Data;
using WeatherForecastAPI.Models;

namespace WeatherForecastAPI.Repository
{
   public class FavoriteLocationRepository 
   {
       private ApplicationDBContext _context;
       private UserRepository _userRepository;

       public FavoriteLocationRepository(ApplicationDBContext context, UserRepository userRepository)
       {
           _context = context;
           _userRepository = userRepository;
       }

       public void AddFavoriteLocation(string locationName, int userId)
       {
           User? user = _userRepository.GetUser(userId);
           if(user == null) return;

           FavoriteLocation location = new FavoriteLocation{ Name = locationName };

           _context.FavoriteLocation.Add(location);
           _context.UserHasLocations.Add(new UserHasLocation { User = user, FavoriteLocation = location });

           _context.SaveChanges();

       }
   }
}
