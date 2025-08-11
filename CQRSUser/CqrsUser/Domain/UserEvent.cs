using System;

namespace CqrsUser.Domain
{
    public class UserEvent
    {
        public Guid EventId { get; set; }
        public Guid UserId { get; set; }
        public string EventType { get; set; } = string.Empty;
        public string EventData { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
