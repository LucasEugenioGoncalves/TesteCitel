using prmToolkit.NotificationPattern;
using System.Threading.Tasks;
using TesteCitel.Domain.Arguments.Accounts;
using TesteCitel.Domain.Interfaces.Services;

namespace TesteCitel.Domain.Services
{
    public class ServiceUser : Notifiable, IServiceUser
    {
        public async Task<AuthorizationResponse> Authentication(AuthenticationRequest authenticationRequest)
        {
            await Task.Delay(5);
            if (string.IsNullOrEmpty(authenticationRequest.Email))
            {
                AddNotification("Email", "O Email é obrigatório.");
                return null;
            }

            if (string.IsNullOrEmpty(authenticationRequest.Password))
            {
                AddNotification("Password", "A Senha é obrigatória.");
                return null;
            }

            return new AuthorizationResponse
            {
                UserId = 1,
                Email = "teste@teste.com",
                Name = "teste"
            };
        }
    }
}
