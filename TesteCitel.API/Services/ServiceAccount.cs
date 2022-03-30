using prmToolkit.NotificationPattern;
using System.Threading.Tasks;
using TesteCitel.API.Interfaces;
using TesteCitel.API.Models;
using TesteCitel.Domain.Arguments.Accounts;
using TesteCitel.Domain.Interfaces.Services;

namespace TesteCitel.API.Services
{
    public class ServiceAccount : Notifiable, IServiceAccount
    {
        private readonly IServiceJwt _serviceJwt;
        private readonly IServiceUser _serviceUser;

        public ServiceAccount(
            IServiceJwt serviceJwt,
            IServiceUser serviceUser)
        {
            _serviceJwt = serviceJwt;
            _serviceUser = serviceUser;
        }

        public async Task<AuthorizationViewModel> Token(AuthenticationViewModel model)
        {
            var request = new AuthenticationRequest
            {
                Email = model.Email,
                Password = model.Password
            };

            var user = await _serviceUser.Authentication(request);
            if (IsInvalid()) return null;
            if (user == null)
            {
                AddNotification("User", "O Usuário não foi encontrado.");
                return null;
            }

            var jwt = _serviceJwt.CreateJsonWebToken(
                user.Email, 
                user.Email ?? "",
                user.UserId.ToString(),
                user.Name);

            return new AuthorizationViewModel
            {
                Id = user.UserId,
                FullName = user.Name,
                Email = user.Email,
                AccessToken = jwt.AccessToken,
                TokenType = jwt.TokenType,
                Expires = jwt.Expires
            };
        }
    }
}
