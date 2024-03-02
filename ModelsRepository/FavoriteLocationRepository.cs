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

       public UserHasLocation? AddFavoriteLocation(int userId, string locationName)
       {
           locationName = locationName.ToLower();

           User? user = _userRepository.GetUser(userId);
           UserHasLocation? userFavLocation = null;

           if(user is not null) 
           {
               FavoriteLocation? location = _context.FavoriteLocation.Where(loc => loc.Name == locationName).FirstOrDefault();

               if(location is null)
               {
                   location = new FavoriteLocation{ Name = locationName };
                   _context.FavoriteLocation.Add(location);

               }

                userFavLocation = new UserHasLocation{ User = user, FavoriteLocation = location };
               _context.UserHasLocations.Add(userFavLocation);
               _context.SaveChanges();
           }

           return userFavLocation;
       }

       public UserHasLocation? DeleteFavoriteLocation(int userId, string locationName)
       {
           UserHasLocation? userFavLocation = _context.UserHasLocations
               .Where(userLocation => 
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
