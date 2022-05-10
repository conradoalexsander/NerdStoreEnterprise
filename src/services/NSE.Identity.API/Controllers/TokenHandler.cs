using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NSE.Identity.API.Extensions;
using NSE.Identity.API.Models;

namespace NSE.Identity.API.Controllers {

    public interface ITokenHandler {
        Task<UserLoginResponse> GenerateJWT(string email);
    }
    
    public class TokenHandler : ITokenHandler {
        private readonly AppSettings _appSettings;
        private readonly UserManager<IdentityUser> _userManager;
        
        public TokenHandler( UserManager<IdentityUser> userManager, IOptions<AppSettings> appSettings) {
            _userManager = userManager;
            _appSettings = appSettings.Value;
        }
        
        public async Task<UserLoginResponse> GenerateJWT(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            
            var claims = await _userManager.GetClaimsAsync(user);
            var userRoles = await _userManager.GetRolesAsync(user);

            AddDefaultUserClaims(user, claims);

            var identityClaims = GetUserRoleClaims(claims, userRoles);

            var encodedToken = GenerateToken(identityClaims);

            return GetTokenResponse(user, claims, encodedToken);
        }

        private UserLoginResponse GetTokenResponse(IdentityUser user, IList<Claim> claims, string encodedToken)
        {
            return new UserLoginResponse
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(_appSettings.ExpirationHours).TotalSeconds.ToString(),
                UserToken = new UserToken
                {
                    Id = user.Id,
                    Email = user.Email,
                    Claims = claims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }

            };
        }

        public string GenerateToken(ClaimsIdentity identityClaims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.ValidIn,
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appSettings.ExpirationHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), algorithm: SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);
            return encodedToken;
        }

        private ClaimsIdentity GetUserRoleClaims(IList<Claim> claims, IList<string> userRoles)
        {
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(type: "role", value: userRole));
            }

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaims(claims);
            return identityClaims;
        }

        private void AddDefaultUserClaims(IdentityUser user, IList<Claim> claims)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString())); //Token expiration
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64)); //Token issue date
        }

        private long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
    }
}