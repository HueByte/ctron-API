using Microsoft.Extensions.Options;
using System.Text;
using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Ctron.API.Configuration;
using Ctron.API.DTO;
using System.Security.Claims;

namespace Ctron.API.Authentication
{
    public class JwtAuthentication : IJwtAuthentication
    {
        private readonly JWTConfig config;
        private readonly IApiAuthentication _apiAuth;

        public JwtAuthentication(IOptions<JWTConfig> _config)
        {
            this.config = _config.Value;
        }
        public VerifiedUser GenerateJsonWebToken(VerifiedUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            //add roles to claims
            foreach(var role in user.Role)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(120),
                SigningCredentials = credentials,
                Issuer = config.Issuer,
                Audience = config.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescription);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }
    }
}