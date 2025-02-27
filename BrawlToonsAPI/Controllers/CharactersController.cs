﻿using Microsoft.AspNetCore.Mvc;
using BrawlToonsAPI.Models;
using System.Numerics;
using Microsoft.EntityFrameworkCore;
namespace BrawlToonsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly GameContext _context;

        public CharactersController(GameContext context)
        {
            _context = context;
        }


        [HttpGet("GET/{id}")]
        public async Task<ActionResult<Characters>> GetCharacter(int id)
        {
            var character = await _context.characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }
            return Ok(character);
        }

        [HttpGet("SortedByWins")]
        public async Task<ActionResult<IEnumerable<Characters>>> GetCharactersSortedByWins()
        {
            var characters = await _context.characters
                .OrderByDescending(c => c.total_wins)
                .ToListAsync();

            return Ok(characters);
        }


        [HttpPut("UpdateWins/{id}")]
        public async Task<IActionResult> UpdateWins(int id)
        {
            var character = await _context.characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            character.total_wins += 1;
            _context.Entry(character).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("UpdateLoses/{id}")]
        public async Task<IActionResult> UpdateLoses(int id)
        {
            var character = await _context.characters.FindAsync(id);
            if (character == null)
            {
                return NotFound();
            }

            character.total_defeats += 1;  
            _context.Entry(character).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
