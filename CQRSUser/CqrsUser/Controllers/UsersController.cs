using CqrsUser.Commands.CreateUser;
using CqrsUser.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using CqrsUser.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CqrsUser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // POST api/users
        [HttpPost]
        public async Task<ActionResult<UserDto>> Create([FromBody] CreateUserCommand command)
        {
            var created = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // GET api/users/{id}
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDto>> GetById(Guid id, [FromServices] AppDbContext db, [FromServices] IMapper mapper)
        {
            var user = await db.Users.FindAsync(id);
            if (user == null) return NotFound();
            return Ok(mapper.Map<UserDto>(user));
        }

        // GET api/users/by-number/{userNumber}
        [HttpGet("by-number/{userNumber:int}")]
        public async Task<ActionResult<UserDto>> GetByUserNumber(int userNumber, [FromServices] AppDbContext db, [FromServices] IMapper mapper)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.UserNumber == userNumber);
            if (user == null) return NotFound();
            return Ok(mapper.Map<UserDto>(user));
        }

        // GET api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll([FromServices] AppDbContext db, [FromServices] IMapper mapper)
        {
            var users = await db.Users
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            var dtos = users.Select(u => mapper.Map<UserDto>(u));
            return Ok(dtos);
        }
    }
}