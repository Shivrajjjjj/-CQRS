using Microsoft.AspNetCore.Mvc;
using CqrsUser.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CqrsUser.Controllers.Mvc
{
	public class UsersMvcController : Controller
	{
		private readonly AppDbContext _db;

		public UsersMvcController(AppDbContext db)
		{
			_db = db;
		}

		// GET: /UsersMvc
		public async Task<IActionResult> Index()
		{
			var users = await _db.Users
				.OrderByDescending(u => u.CreatedAt)
				.ToListAsync();

			return View("~/Views/Users/Index.cshtml", users);
		}

		// GET: /UsersMvc/Details/{id}
		public async Task<IActionResult> Details(Guid id)
		{
			var user = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
			if (user == null) return NotFound();

			return View("~/Views/Users/Details.cshtml", user);
		}

		// Optional: GET /UsersMvc/ByNumber/{userNumber}
		[HttpGet("/UsersMvc/ByNumber/{userNumber:int}")]
		public async Task<IActionResult> ByNumber(int userNumber)
		{
			var user = await _db.Users.FirstOrDefaultAsync(u => u.UserNumber == userNumber);
			if (user == null) return NotFound();

			return View("~/Views/Users/Details.cshtml", user);
		}
	}
}