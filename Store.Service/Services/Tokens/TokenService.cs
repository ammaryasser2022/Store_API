using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Store.Core.Identity;
using Store.Core.Services.Contract;
using Microsoft.IdentityModel.Tokens;

namespace Store.Service.Services.Tokens
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<string> CreateTokenAsync(AppUser user , UserManager<AppUser> userManager)
        {
            // 1. Header --> algo Typr
            // 2. Claims -- Payload -- Data 
            // 3. Signature (Header + Payload )

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.DisplayName),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),

            };

            //momken adef role in claims
            var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim( ClaimTypes.Role, role)) ;
            }

            var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
                claims: authClaims,
                signingCredentials : new SigningCredentials(authkey , SecurityAlgorithms.HmacSha256Signature )  
                );

            return new JwtSecurityTokenHandler().WriteToken(token); // this generate token and return it 


        }
    }
}
