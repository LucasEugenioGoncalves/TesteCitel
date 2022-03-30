using System.Threading.Tasks;
using TesteCitel.API.Models;
using TesteCitel.Domain.Interfaces.Services.Base;

namespace TesteCitel.API.Interfaces
{
    public interface IServiceAccount : IServiceBase
    {
        Task<AuthorizationViewModel> Token(AuthenticationViewModel model);
    }
}
