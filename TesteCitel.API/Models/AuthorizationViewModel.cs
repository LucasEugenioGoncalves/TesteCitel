using System;

namespace TesteCitel.API.Models
{
    public class AuthorizationViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string UserGroup { get; set; }

        public string AccessToken { get; set; }

        public string TokenType { get; set; }
        public string[] GroupScreens { get; set; }

        public DateTime Expires { get; set; }
    }
}
