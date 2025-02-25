using Microsoft.AspNetCore.Mvc;
using BrawlToonsAPI.Models;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
namespace BrawlToonsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchesController : ControllerBase
    {
        private readonly GameContext _context;

        public MatchesController(GameContext context)
        {
            _context = context;
        }
        [HttpPost("PostMatch")]
        public async Task<ActionResult<Player>> AddPlayer(Matches match)
        {
            _context.matches.Add(match);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMatch), new { id = match.match_id }, match);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Matches>> GetMatch(int id)
        {
            var match = await _context.matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            return Ok(match);
        }

        [HttpGet("last/{playerId}")]
        public async Task<ActionResult<IEnumerable<Matches>>> GetLast4MatchesByPlayer(int playerId)
        {
            List<Matches> matches = await _context.matches
                .Where(m => m.player_1_id == playerId || m.player_2_id == playerId)
                .OrderByDescending(m => m.match_id)
                .Take(4)
                .ToListAsync();

            if (matches == null || !matches.Any())
            {
                return NotFound();
            }

            return Ok(matches);
        }
    }
}
