using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OsvitaWebApiPL.Identity;
using OsvitaWebApiPL.Interfaces;
using OsvitaWebApiPL.Models;

namespace OsvitaWebApiPL.Services
{
	public class IdentityService : IIdentityService
	{
        private readonly UserManager<OsvitaUser> userManager;
        private readonly JwtSettings jwtSettings;

        public IdentityService(UserManager<OsvitaUser> userManager, IOptions<JwtSettings> jwtSettings)
        {
            this.userManager = userManager;
            this.jwtSettings = jwtSettings.Value;
        }

        public async Task RegisterUser(UserDTO userDto)
        {
            var user = new OsvitaUser { Email = userDto.Email, UserName = userDto.Email, FirstName = userDto.FirstName, SecondName = userDto.SecondName };
            var result = await userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                throw new Exception();
            }

            if (!isAdminRoleAvailable())
            {
                var adminRole = userDto.Roles.FirstOrDefault(x => x.ToUpper() == RoleSettings.NormalizedAdminRole);
                if (!String.IsNullOrEmpty(adminRole))
                {
                    userDto.Roles.Remove(adminRole);
                }
            }

            await userManager.AddToRolesAsync(user, userDto.Roles);
        }

        public async Task<string> LoginUser(UserLoginDTO userLoginDto)
        {
            if (!await ValidateUser(userLoginDto))
            {
                throw new Exception();
            }
            var user = await userManager.FindByNameAsync(userLoginDto.Email);
            return await CreateToken(user);
        }

        private async Task<bool> ValidateUser(UserLoginDTO userLoginDto)
        {
            var user = await userManager.FindByNameAsync(userLoginDto.Email);
            if (user is not null && await userManager.CheckPasswordAsync(user, userLoginDto.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async Task<string> CreateToken(OsvitaUser user)
        {
            var signingCredentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            var token = GenerateToken(signingCredentials, claims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GenerateToken(SigningCredentials signingCredentials, List<Claim> claims)
        {
            var expiration = DateTime.Now.AddMinutes(jwtSettings.Lifetime);

            var token = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
                );
            return token;
        }

        private async Task<List<Claim>> GetClaims(OsvitaUser user)
        {
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, user.UserName)
            };
            var roles = await userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            return claims;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private bool isAdminRoleAvailable()
        {
            return false;
        }
    }
}

