using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WebTalkApi.Services
{
    public interface IJwtGenerator
    {
        public string GetJwtToken(List<Claim> claims);
    }
     
    public class JwtGenerator: IJwtGenerator
    {
        private readonly AuthenticationSettings _authenticationSettings;

        public JwtGenerator(AuthenticationSettings authenticationSettings)
        {
            _authenticationSettings = authenticationSettings;
        }
        public string GetJwtToken(List<Claim> claims)
        {
            var jwtKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(jwtKey, SecurityAlgorithms.HmacSha256);

            var expirationDate = DateTime.Now.AddDays(_authenticationSettings.ExpireDays);


            var jwtToken = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expirationDate,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.WriteToken(jwtToken);
        }
    }
}
