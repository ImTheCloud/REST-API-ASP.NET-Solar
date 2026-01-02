using PROJETDOTNETQ5.Models;

namespace PROJETDOTNETQ5.Services
{
    // Interface for the user service
    public interface IUserService
    {
        // Method to retrieve a user based on login credentials
        public User Get(UserLogin userLogin);
    }
}
