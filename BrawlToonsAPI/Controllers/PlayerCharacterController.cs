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
        [HttpGet("GET/{player_id},{character_id}")]
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

        [HttpPost("POST")]
        public async Task<ActionResult<PlayerCharacter>> AddPlayerCharacter(PlayerCharacter pc)
        {
            _context.playerCharacters.Add(pc);
            await _context.SaveChangesAsync();

            // Utiliza el nombre de la acción y los parámetros para crear la ruta correcta
            return CreatedAtAction(
                nameof(GetPlayerCharacterStats),
                new { player_id = pc.player_id, character_id = pc.character_id },
                pc
            );
        }
        [HttpPut("UPDATE")]
        public async Task<IActionResult> Update([FromBody] PlayerCharacter playerCharUpdated)
        {
            var playerCharacter = await _context.playerCharacters
                .FirstOrDefaultAsync(pc => pc.player_id == playerCharUpdated.player_id && pc.character_id == playerCharUpdated.character_id);
            if (playerCharacter == null)
            {
                return NotFound();
            }

            // Actualiza las propiedades del objeto encontrado
            _context.Entry(playerCharacter).CurrentValues.SetValues(playerCharUpdated);

            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
