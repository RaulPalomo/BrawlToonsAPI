using Microsoft.AspNetCore.Mvc;
using BrawlToonsAPI.Models;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
namespace BrawlToonsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerCharacterController : ControllerBase
    {
        private readonly GameContext _context;

        public PlayerCharacterController(GameContext context)
        {
            _context = context;
        }
        [HttpGet("{player_id},{character_id}")]
        //metodo que recoje las stats del jugador con el personaje
        public async Task<ActionResult<PlayerCharacter>> GetPlayerCharacterStats(int player_id, int character_id)
        {
            var playerCharacter = await _context.playerCharacters
                .FirstOrDefaultAsync(pc => pc.player_id == player_id && pc.character_id == character_id);

            if (playerCharacter == null)
            {
                return NotFound("No se encontraron estadísticas para el jugador y personaje especificados.");
            }

            return Ok(playerCharacter);
        }
    }
}
