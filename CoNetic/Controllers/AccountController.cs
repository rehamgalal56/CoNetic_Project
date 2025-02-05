using CoNetic.Core.Models;
using CoNetic.Core.ServicesInterfaces;
using CoNetic.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Google.Apis.Auth;

namespace CoNetic.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _singInManger;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public AccountController(UserManager<User> userManager , 
            SignInManager<User> singInManger,
            ITokenService tokenService,
            IConfiguration configuration
            )
        {
            _userManager = userManager;
            _singInManger = singInManger;
            _tokenService = tokenService;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            if(await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return BadRequest("User with this email already exists");
            }

            if(model.Password != model.ConfirmPassword)
                return BadRequest("The Password and ConfirmPassword dont Matching");


            var user = new User()
            {
                FirstName = model.FullName.Split(" ")[0],
                LastName = model.FullName.Contains(" ") ? model.FullName.Substring(model.FullName.IndexOf(" ") + 1) : "",
                UserName = model.FullName.Replace(" ",""),
                Email = model.Email,
                FullName = model.FullName
                
            };
            var Result = await _userManager.CreateAsync(user, model.Password);

            if (!Result.Succeeded)
                return BadRequest(Result.Errors);

            var ReturnedUser = new UserDto
            {
                FullName = model.FullName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user,_userManager)
            };
            return Ok(ReturnedUser);

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null)
            {
                return Unauthorized("User with this email does not exist");
            }
            var result = await _singInManger.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded)
            {
                return Unauthorized("Invalid credentials");
            }

            var ReturnedUser = new UserDto
            {
                FullName = user.FullName,
                Email = user.Email,
                Token = await _tokenService.CreateTokenAsync(user, _userManager)
                
            };
            return Ok(ReturnedUser);


        }

        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginDto model)
        {
            var payload = await VerifyGoogleToken(model);
            if (payload == null)
                return BadRequest("Invalid Google Token");

            var user = await _userManager.FindByEmailAsync(payload.Email);
            if (user == null)
            {
                user = new User
                {
                    UserName = payload.Email,
                    Email = payload.Email,
                    FirstName = payload.GivenName,
                    LastName = payload.FamilyName,
                    FullName = payload.Name
                };

                var result = await _userManager.CreateAsync(user);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);
            }

            var token = await _tokenService.CreateTokenAsync(user, _userManager);
            return Ok(new { Token = token });
        }
        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleToken(GoogleLoginDto model)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string> { _configuration["Google:ClientId"] }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(model.IdToken, settings);
                return payload;
            }
            catch
            {
                return null;
            }
        }


    }
}
