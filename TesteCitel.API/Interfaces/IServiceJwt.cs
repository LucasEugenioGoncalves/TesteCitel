using prmToolkit.NotificationPattern;
using TesteCitel.API.Models.JWT;

namespace TesteCitel.API.Interfaces
{
    public interface IServiceJwt : INotifiable
    {
        JsonWebTokenCustom CreateJsonWebToken(string email, string username, string userId, string name);
    }
}
