using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OsvitaBLL.Interfaces;
using OsvitaBLL.Models;
using OsvitaBLL.Services;
using OsvitaWebApiPL.Interfaces;
using OsvitaWebApiPL.Models;

namespace OsvitaWebApiPL.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService identityService;
        private readonly IUserService userService;

        public AccountController(IIdentityService identityService, IUserService userService)
        {
            this.identityService = identityService;
            this.userService = userService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                await identityService.RegisterUser(userDto);

                var userModel = new UserModel { Email = userDto.Email, FirstName = userDto.FirstName, SecondName = userDto.SecondName };
                await userService.AddAsync(userModel);

                return Accepted();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            try
            {
                int userId = (await userService.GetByEmailAsync(userLoginDto.Email)).Id;
                var resultToken = await identityService.LoginUser(userLoginDto);

                return Accepted(new
                {
                    Token = resultToken,
                    Id = userId
                });
            }
            catch (Exception)
            {
                return Unauthorized(userLoginDto);
            }
        }
    }
}
