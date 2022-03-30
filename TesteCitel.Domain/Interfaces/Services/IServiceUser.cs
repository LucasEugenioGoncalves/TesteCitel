using System.Threading.Tasks;
using TesteCitel.Domain.Arguments.Accounts;
using TesteCitel.Domain.Interfaces.Services.Base;

namespace TesteCitel.Domain.Interfaces.Services
{
    public interface IServiceUser : IServiceBase
    {
        Task<AuthorizationResponse> Authentication(AuthenticationRequest authenticationRequest);
    }
}
