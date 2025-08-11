using System.Text.Json;
using CqrsUser.Domain;

namespace CqrsUser.Infrastructure
{
    public class EventStore
    {
        private readonly AppDbContext _db;

        public EventStore(AppDbContext db)
        {
            _db = db;
        }

        public async Task SaveEventAsync(string eventType, object entity)
        {
            var userId = Guid.Empty;
            if (entity is User u) userId = u.Id;

            var ev = new UserEvent
            {
                EventId = Guid.NewGuid(),
                UserId = userId,
                EventType = eventType,
                EventData = JsonSerializer.Serialize(entity),
                CreatedAt = DateTime.UtcNow
            };

            _db.UserEvents.Add(ev);
            await _db.SaveChangesAsync();
        }
    }
}
