using BugTicketingSystem.BL.DTOs.Account;
using BugTicketingSystem.BL.Managers.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BugTicketingSystem.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<Results<Ok<string>,BadRequest<string>>> Register(RegisterDto register)
        {
            var result = await _accountManager.Register(register);
            if (result == "User registered and assigned role successfully.")
            {
                return TypedResults.Ok(result);
            }
            else
            {
                return TypedResults.BadRequest(result);
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<Results<Ok<LoginResultDto>, BadRequest<string>>> Login(LoginDto login)
        {
            var result = await _accountManager.Login(login);

            if (result == null || string.IsNullOrEmpty(result.Token))
            {
                return TypedResults.BadRequest("Invalid username or password");
            }

            return TypedResults.Ok(result);
        }
    }
}
