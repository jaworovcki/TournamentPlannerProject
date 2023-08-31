using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TournamentPlanner.Data;

namespace TournamentPlanner.Controllers
{
	[ApiController]
	[Route("api/players")]
	public class PlayersController : ControllerBase
	{
		private readonly TournamentPlannerDbContext _dbContext;

		public PlayersController(TournamentPlannerDbContext dbContext)
        {
			_dbContext = dbContext;
		}

		[HttpGet]
		public async Task<IList<Player>> GetAllPlayersAsync([FromQuery] string? name = null)
			=> await _dbContext.GetFilteredPlayers(name);

		[HttpPost]
		public async Task<Player> AddPlayerAsync([FromBody] Player newPlayer) 
			=> await _dbContext.AddPlayer(newPlayer);

		

	}
}
