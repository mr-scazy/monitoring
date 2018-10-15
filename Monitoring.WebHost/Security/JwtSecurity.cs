using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace Monitoring.WebHost.Security
{
    public static class JwtSecurity
    {
        /// <summary>
        /// Создать токен
        /// </summary>
        /// <param name="claims">Список требований</param>
        public static JwtSecurityToken CreateToken(IEnumerable<Claim> claims)
        {
            var now = DateTime.UtcNow;

            var token = new JwtSecurityToken(
                notBefore: now,
                issuer: AuthOptions.Issuer,
                audience: AuthOptions.Audience,
                claims: claims,
                expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
                signingCredentials: new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(), 
                    SecurityAlgorithms.HmacSha256));

            return token;
        }
    }
}
