using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebTalkApi.Models;
using WebTalkApi.Services;

namespace WebTalkApi.Controllers
{

    [Route("api/[Controller]")]
    [ApiController]

    public class AccountController:ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterUserDto registerDto)
        {
            _accountService.RegisterUser(registerDto);

            return Ok();
        }

        [HttpPost("Login")]
        public ActionResult Login([FromBody] LoginUserDto loginDto)
        {
            string token = _accountService.LoginUser(loginDto);

            return Ok(token);
        }
    }
}
