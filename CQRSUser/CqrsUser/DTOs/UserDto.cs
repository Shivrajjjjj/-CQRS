using System;

namespace CqrsUser.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public int UserNumber { get; set; } // New short ID
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
