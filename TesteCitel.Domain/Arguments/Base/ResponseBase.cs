using prmToolkit.NotificationPattern;
using System.Collections.Generic;

namespace TesteCitel.Domain.Arguments.Base
{
    public class ResponseBase
    {
        public ResponseBase(
            string message = "Operação realizada com sucesso.",
            bool success = true,
            IEnumerable<Notification> notifications = null)
        {
            Message = message;
            Success = success;
            Notifications = notifications ?? new List<Notification>();
        }

        public string Id { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }

        public IEnumerable<Notification> Notifications { get; set; }
    }
}
