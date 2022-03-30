namespace TesteCitel.Domain.Arguments.Accounts
{
    public class AuthorizationResponse
    {
        public int UserId { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string UserGroup { get; set; }

        public int? ClienteId { get; set; }

        public int? ParceiroId { get; set; }
        public int[] FarmIds { get; set; }
        public string[] GroupScreens { get; set; }
    }
}
