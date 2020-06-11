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
using Newtonsoft.Json;

namespace MusicSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongsController : ControllerBase
    {
        private readonly ISongsService songsService;
        private readonly ISongsPerformersService songsPerformersService;
        private readonly IAlbumsService albumsServicec;
        private readonly IWritersService writersService;

        public SongsController(ISongsService songsService, ISongsPerformersService songsPerformersService, IAlbumsService albumsServicec, IWritersService writersService)
        {
            this.songsService = songsService;
            this.songsPerformersService = songsPerformersService;
            this.albumsServicec = albumsServicec;
            this.writersService = writersService;
        }

        // GET: api/Songs
        [HttpGet]
        public ActionResult<IEnumerable<SongDto>> GetSongs()
        {
            return this.songsService.GetAll<SongDto>().ToList();
        }

        // GET: api/Songs/5
        [HttpGet("{id}")]
        public ActionResult<SongDto> GetSong(int id)
        {
            var exists = this.songsService.Exists(id);

            if (!exists)
            {
                return this.NotFound();
            }

            var song = this.songsService.GetById<SongDto>(id);

            return song;
        }

        // PUT: api/Songs/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, SongDto song)
        {
            song.Id = id;

            if (!this.ModelState.IsValid)
                return this.BadRequest();

            var isUnique = this.songsService.IsUnique(song.Name, id);
            if (isUnique == false)
                return this.BadRequest("Already song with that name");

            var result = await this.songsService.Update(id, song);

            if(result == false)
                return this.NotFound();


            return NoContent();
        }

        // POST: api/Songs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SongDto>> PostSong(SongDto song)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            if(!this.albumsServicec.Exists((int)song.AlbumId) || !this.writersService.Exists((int)song.WriterId))
            {
                return this.BadRequest();
            }
            
            var resultId = await this.songsService.Add(song);

            if (resultId == -1)
                return this.BadRequest("Already song with that name");

            song.Id = resultId;

            return CreatedAtAction("GetSong", new { id = resultId }, song);
        }

        // DELETE: api/Songs/5
        [HttpDelete("{id}")]
        public ActionResult<SongDto> DeleteSong(int id)
        {
            var song = this.songsService.GetById<SongDto>(id);
            if (song == null)
            {
                return this.NotFound();
            }

            this.songsPerformersService.DeleteBySongIdAsync(id);
            this.songsService.DeleteBySongId(id);

            return song;
        }

    }
}
