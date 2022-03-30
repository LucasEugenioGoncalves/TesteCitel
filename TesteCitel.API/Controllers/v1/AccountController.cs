using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using TesteCitel.API.Controllers.Base;
using TesteCitel.API.Interfaces;
using TesteCitel.API.Models;
using TesteCitel.Infra.Transactions;

namespace TesteCitel.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IServiceAccount _serviceAccount;
        public AccountController(IUnitOfWork unitOfWork, IServiceAccount serviceAccount ) : base(unitOfWork) 
        {
            _serviceAccount = serviceAccount;
        }

        [HttpPost]
        [Route("Token")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Token([FromBody] AuthenticationViewModel authentication)
        {
            try
            {
                if (!ModelState.IsValid) return ModelStateErrors();

                var token = await _serviceAccount.Token(authentication);
                return ResponseAsync(token, _serviceAccount);
            }
            catch (Exception ex)
            {
                return ResponseException(ex);
            }
        }
    }
}
