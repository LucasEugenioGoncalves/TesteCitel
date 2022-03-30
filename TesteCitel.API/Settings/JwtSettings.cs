using System;
using System.Text;

namespace TesteCitel.API.Settings
{
    public class JwtSettings
    {
        public int ValidForHours { get; set; }

        public string Secret { get; set; }

        public DateTime IssuedAt => DateTime.Now;

        public DateTime AccessTokenExpiration => IssuedAt + TimeSpan.FromHours(ValidForHours);

        public byte[] Key => Encoding.ASCII.GetBytes(Secret);
    }
}
