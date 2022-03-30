using System;

namespace TesteCitel.API.Models.JWT
{
    public class JsonWebTokenCustom
    {
        public string AccessToken { get; set; }

        public string TokenType { get; set; } = "Bearer";

        public DateTime Expires { get; set; }
    }
}
