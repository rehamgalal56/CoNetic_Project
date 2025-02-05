using CoNetic.Core.Models;
using CoNetic.Core.ServicesInterfaces;
using CoNetic.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoNetic.Services.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly JWT jwt;
        public TokenService(IConfiguration configuration, IOptions<JWT> jwt)
        {
            this.configuration = configuration;
            this.jwt = jwt.Value;

        }

        public async Task<string> CreateTokenAsync(User User, UserManager<User> userManager)
        {
            Claim[] Claims =
           [
               new(JwtRegisteredClaimNames.Sub,User.Id),
                new(JwtRegisteredClaimNames.Email,User.Email!),
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())

           ];

            //var UserRoles = await userManager.GetRolesAsync(User);
            //foreach (var Role in UserRoles)
            //{
            //    AuthClaims.Add(new Claim(ClaimTypes.Role, Role));
            //}
            var AuthKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var Token = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: Claims,
                expires: DateTime.Now.AddDays(jwt.DurationInDays),
                signingCredentials: new SigningCredentials(AuthKey, SecurityAlgorithms.HmacSha256Signature)

                );
            return new JwtSecurityTokenHandler().WriteToken(Token);
        }
    }
}