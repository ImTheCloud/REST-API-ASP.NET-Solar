using PROJETDOTNETQ5.Models;
using PROJETDOTNETQ5.Repositories;

namespace PROJETDOTNETQ5.Services
{
    // Service class for handling operations related to users
    public class UserService : IUserService
    {
        // Get user based on login credentials
        public User Get(UserLogin userLogin)
        {
            User user = UserRepository.Users.FirstOrDefault(o => o.Username.Equals(userLogin.Username, StringComparison.OrdinalIgnoreCase) && o.Password.Equals(userLogin.Password));

            return user;
        }
    }
}
