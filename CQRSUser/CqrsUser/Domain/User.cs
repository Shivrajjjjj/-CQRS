using System;

namespace CqrsUser.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public int UserNumber { get; set; } // New short ID
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
