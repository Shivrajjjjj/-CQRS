using CqrsUser.DTOs;
using MediatR;

namespace CqrsUser.Commands.CreateUser
{
    public record CreateUserCommand(string FirstName, string LastName, string Email) : IRequest<UserDto>;
}