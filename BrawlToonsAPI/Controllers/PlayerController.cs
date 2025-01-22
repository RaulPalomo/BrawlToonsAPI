using Microsoft.AspNetCore.Mvc;
using BrawlToonsAPI.Models;
using System.Numerics;
using Microsoft.EntityFrameworkCore;

namespace BrawlToonsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly GameContext _context;

        public PlayerController(GameContext context)
        {
            _context = context;
        }

        // GET: api/players/{id}
        [HttpGet("GET/{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            return Ok(player);
        }

        // POST: api/players
        [HttpPost]
        public async Task<ActionResult<Player>> AddPlayer(Player player)
        {
            _context.players.Add(player);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPlayer), new { id = player.player_id }, player);
        }

        // verify password GET
        [HttpGet ("GET/{username},{password}")]
        public async Task<ActionResult<Player>> VerifyUser(string username, string password)
        {
            var player = await _context.players.FirstOrDefaultAsync(p => p.username == username); ;
            if (player == null)
            {
                return NotFound("Usuario no encontrado");
            }

            // Verifica la contraseña (en un escenario real, debería ser un hash)
            if (player.password != password)
            {
                return Unauthorized("Contraseña incorrecta");
            }

            return Ok(player);
        }

        [HttpPut("UPDATE")]
        public async Task<IActionResult> Update([FromBody] Player playerUpdated)
        {
            var player = await _context.players.FindAsync(playerUpdated.player_id);
            if (player == null)
            {
                return NotFound();
            }

            // Actualiza las propiedades del objeto encontrado
            _context.Entry(player).CurrentValues.SetValues(playerUpdated);

            await _context.SaveChangesAsync();
            return NoContent();
        }


    }
}
