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

       public User? AddFavoriteLocation(int userId, string locationName)
       {
           User? user = _userRepository.GetUser(userId);
           if(user is not null) 
           {
               FavoriteLocation location = new FavoriteLocation{ Name = locationName.ToLower() };

               _context.FavoriteLocation.Add(location);
               _context.UserHasLocations.Add(new UserHasLocation { User = user, FavoriteLocation = location });

               _context.SaveChanges();
           }

           return user;
       }

       public UserHasLocation? DeleteFavoriteLocation(int userId, string locationName)
       {
           UserHasLocation? userFavLocation = _context.UserHasLocations.Where(userLocation => 
                userLocation.UserId == userId && userLocation.FavoriteLocation.Name == locationName)
               .FirstOrDefault();

           if(userFavLocation is not null)
           {
               _context.UserHasLocations.Remove(userFavLocation);

               _context.SaveChanges();
           }

           return userFavLocation;
       }
   }
}
