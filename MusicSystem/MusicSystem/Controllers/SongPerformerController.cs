using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSystem.Data;
using MusicSystem.Data.Models;
using MusicSystem.DTOs;
using MusicSystem.Services.Interfaces;

namespace MusicSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class SongPerformerController : ControllerBase
    {
        private readonly ISongsPerformersService songsPerformersService;
        private readonly ISongsService songsService;
        private readonly IPerformerService performerService;

        public SongPerformerController(ISongsPerformersService songsPerformersService, ISongsService songsService, IPerformerService performerService)
        {
            this.songsPerformersService = songsPerformersService;
            this.songsService = songsService;
            this.performerService = performerService;
        }

        // GET: api/SongPerformer
        [HttpGet]
        public ActionResult<IEnumerable<SongPerformerDto>> GetSongsPerformers()
        {
            return this.songsPerformersService.GetAll<SongPerformerDto>().ToList();
        }

        // GET: api/SongPerformer/5
        [HttpGet("{id}")]
        public ActionResult<SongPerformerDto> GetSongPerformer(int id)
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
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutSongPerformer(int id, SongPerformerDto songPerformer)
        //{
        //    if (!this.ModelState.IsValid)
        //    {
        //        return this.BadRequest();
        //    }

        //    if (!this.songsService.Exists((int)songPerformer.SongId) || !this.performerService.Exists((int)songPerformer.PerformerId))
        //    {
        //        return this.NotFound();
        //        return StatusCode((int)HttpStatusCode.Conflict);
        //    }

        //    await this.songsPerformersService.Update(id, songPerformer);

        //    return this.NoContent();
        //}

        // POST: api/SongPerformer
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<SongPerformerDto>> PostSongPerformer(SongPerformerDto songPerformer)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
                
            }
            if (!this.songsService.Exists((int)songPerformer.SongId) || !this.performerService.Exists((int)songPerformer.PerformerId))
            {
                return this.BadRequest();
            }

                //return StatusCode((int)HttpStatusCode.Conflict);

            var id = await this.songsPerformersService.Add(songPerformer);
            songPerformer.Id = id;

            return CreatedAtAction("GetSongPerformer", new { id = id }, songPerformer);
        }

        // DELETE: api/SongPerformer/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SongPerformerDto>> DeleteSongPerformer(int id)
        {
            var exists = this.songsPerformersService.Exists(id);
            if (!exists)
            {
                return this.NotFound();
            }

            var songPerformer = this.songsPerformersService.GetById<SongPerformerDto>(id);
            await this.songsPerformersService.DeleteAsync(id);

            return songPerformer;
        }
    }
}
