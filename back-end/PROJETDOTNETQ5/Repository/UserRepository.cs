using PROJETDOTNETQ5.Models;

namespace PROJETDOTNETQ5.Repositories
{
    // Repository class for managing user data
    public class UserRepository
    {
        // Static list of users for demonstration purposes
        public static List<User> Users = new()
        {
            new()
            {
                Username = "claudiu",
                EmailAddress = "claudiu@email.com",
                Password = "claudiu",
                GivenName = "Claudiu",
                Surname = "TheCloud",
                Role = "Administrator",
            },
            new()
            {
                Username = "medol",
                EmailAddress = "medol@email.com",
                Password = "medol",
                GivenName = "Medol",
                Surname = "TheBestTeacher",
                Role = "Administrator",
            },
            new()
            {
                Username = "user",
                EmailAddress = "user@email.com",
                Password = "user",
                GivenName = "User",
                Surname = "leponge",
                Role = "Standard",
            },
        };
    }
}