using Microsoft.IdentityModel.Tokens;
using prmToolkit.NotificationPattern;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using TesteCitel.API.Interfaces;
using TesteCitel.API.Models.JWT;
using TesteCitel.API.Settings;

namespace TesteCitel.API.Services
{
    public class ServiceJwt : Notifiable, IServiceJwt
    {
        private readonly JwtSettings _settings;

        public ServiceJwt(JwtSettings settings)
        {
            _settings = settings;
        }

        public JsonWebTokenCustom CreateJsonWebToken(string email, string username, string userId, string name)
        {
            var identity = GetClaimsIdentity(email, username, userId, name);
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.Secret);

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Subject = identity,
                NotBefore = _settings.IssuedAt,
                Expires = _settings.AccessTokenExpiration,

            });
            var accessToken = handler.WriteToken(securityToken);

            return new JsonWebTokenCustom
            {
                AccessToken = accessToken,
                Expires = _settings.AccessTokenExpiration
            };
        }

        private ClaimsIdentity GetClaimsIdentity(
            string email,
            string username,
            string userId,
            string name)
        {
            var claimsIdentity = new ClaimsIdentity
            (
                new GenericIdentity(username),
                new[] {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("Email", email),
                new Claim("UserName", username),
                new Claim("Name", name),
                new Claim("UserId", userId)}
            );
            return claimsIdentity;
        }
    }
}
