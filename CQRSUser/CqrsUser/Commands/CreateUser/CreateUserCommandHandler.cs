using AutoMapper;
using CqrsUser.Domain;
using CqrsUser.DTOs;
using CqrsUser.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CqrsUser.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly EventStore _eventStore;

        public CreateUserCommandHandler(AppDbContext context, IMapper mapper, EventStore eventStore)
        {
            _context = context;
            _mapper = mapper;
            _eventStore = eventStore;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // Check for existing user with same email
            var existingUser = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

            if (existingUser != null)
            {
                throw new InvalidOperationException("User with this email already exists.");
            }

            var user = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken); // UserNumber is populated here

            // Save event for Event Sourcing
            await _eventStore.SaveEventAsync("UserCreated", user);

            return _mapper.Map<UserDto>(user);
        }
    }
}