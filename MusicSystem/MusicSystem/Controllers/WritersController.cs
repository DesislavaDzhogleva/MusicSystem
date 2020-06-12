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
using Microsoft.AspNetCore.Authorization;

namespace MusicSystem.Controllers
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class WritersController : ControllerBase
    {
        private readonly IWritersService writersService;
        private readonly ISongsService songsService;

        public WritersController(IWritersService writersService, ISongsService songsService)
        {
            this.writersService = writersService;
            this.songsService = songsService;
        }

        // GET: api/WriterDtoes
        [HttpGet]
        public ActionResult<IEnumerable<WriterDto>> GetWriter(string name)
        {
            if(name == null)
                return this.writersService.GetAll<WriterDto>().ToList();

            return this.writersService.GetByName<WriterDto>(name).ToList();
        }

        // GET: api/WriterDtoes/5
        [HttpGet("{id}")]
        public ActionResult<WriterDto> GetWriter(int id)
        {
            var exists = this.writersService.Exists(id);

            if (!exists)
            {
                return this.NotFound();
            }

            var producer = this.writersService.GetById<WriterDto>(id);

            return producer;
        }

        // PUT: api/WriterDtoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWriter(int id, WriterDto writerDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            writerDto.Id = id;


            var result = await this.writersService.Update(id, writerDto);

            if (result == false)
                return this.BadRequest();


            return CreatedAtAction("GetWriter", new { id = id }, writerDto);
        }

        // POST: api/WriterDtoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<WriterDto>> PostWriter(WriterDto writerDto)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var result = await this.writersService.Add(writerDto);
            writerDto.Id = result;

            return CreatedAtAction("GetWriter", new {id = result}, writerDto);
        }

        // DELETE: api/WriterDtoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WriterDto>> DeleteWriter(int id)
        {
            var writerDto = this.writersService.GetById<WriterDto>(id);
            if (writerDto == null)
            {
                return this.NotFound();
            }

            await this.songsService.DeleteByWriterId(id);
            await this.writersService.Delete(id);

            return writerDto;
        }
    }
}
