using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicSystem.DTOs;
using MusicSystem.Data;
using MusicSystem.Services.Interfaces;

namespace MusicSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumsService albumsService;
        private readonly ISongsService songsService;

        public AlbumsController(IAlbumsService albumsService, ISongsService songsService)
        {
            this.albumsService = albumsService;
            this.songsService = songsService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlbumDto>> GetAlbum()
        {
            return this.albumsService.GetAll<AlbumDto>().ToList();
        }

        [HttpGet("{id}")]
        public ActionResult<AlbumDto> GetAlbum(int id)
        {
            var exists = this.albumsService.Exists(id);

            if (!exists)
            {
                return this.NotFound();
            }

            var album = this.albumsService.GetById<AlbumDto>(id);

            return album;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlbum(int id, AlbumDto albumDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            albumDto.Id = id;


            var result = await this.albumsService.Update(id, albumDto);

            if (result == false)
                return this.BadRequest("No such album");

            var isQunie = this.albumsService.IsUnique(albumDto.Name, id);
            if (isQunie == false)
                return this.BadRequest("There is already album with that name in the database");

            return CreatedAtAction("GetAlbum", new { id = id }, albumDto);
        }
    
        [HttpPost]
        public async Task<ActionResult<AlbumDto>> PostAlbum(AlbumDto albumDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var id = await this.albumsService.AddAsync(albumDto);

            if (id == -1)
                return this.BadRequest("Already Exists Album With That Name");

            albumDto.Id = id;
            return CreatedAtAction("GetAlbum", new { id = id }, albumDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<AlbumDto>> DeleteAlbum(int id)
        {
            var album = this.albumsService.GetById<AlbumDto>(id);
            if (album == null)
            {
                return this.NotFound();
            }

            await this.songsService.DeleteByAlbumId(id);
            await this.albumsService.Delete(album.Id);

            return album;
        }

    }
}
