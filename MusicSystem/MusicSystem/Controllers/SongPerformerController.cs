using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSystem.Data;
using MusicSystem.Data.Models;
using MusicSystem.DTOs;
using MusicSystem.Services.Interfaces;

namespace MusicSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SongPerformerController : ControllerBase
    {
        private readonly ISongsPerformersService songsPerformersService;
        private readonly ApplicationDbContext _context;


        public SongPerformerController(ISongsPerformersService songsPerformersService)
        {
            this.songsPerformersService = songsPerformersService;
        }

        // GET: api/SongPerformer
        [HttpGet]
        public ActionResult<IEnumerable<SongPerformerDto>> GetSongsPerformers()
        {
            return this.songsPerformersService.GetAll<SongPerformerDto>().ToList();
        }

        // GET: api/SongPerformer/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SongPerformerDto>> GetSongPerformer(int id)
        {
            var songPerformer = this.songsPerformersService.GetById<SongPerformerDto>(id);

            if (songPerformer == null)
            {
                return NotFound();
            }

            return songPerformer;
        }

        // PUT: api/SongPerformer/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSongPerformer(int id, SongPerformer songPerformer)
        {
            if (id != songPerformer.SongId)
            {
                return BadRequest();
            }

            _context.Entry(songPerformer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongPerformerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/SongPerformer
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SongPerformer>> PostSongPerformer(SongPerformer songPerformer)
        {
            _context.SongsPerformers.Add(songPerformer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SongPerformerExists(songPerformer.SongId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSongPerformer", new { id = songPerformer.SongId }, songPerformer);
        }

        // DELETE: api/SongPerformer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SongPerformer>> DeleteSongPerformer(int id)
        {
            var songPerformer = await _context.SongsPerformers.FindAsync(id);
            if (songPerformer == null)
            {
                return NotFound();
            }

            _context.SongsPerformers.Remove(songPerformer);
            await _context.SaveChangesAsync();

            return songPerformer;
        }

        private bool SongPerformerExists(int id)
        {
            return _context.SongsPerformers.Any(e => e.SongId == id);
        }
    }
}
