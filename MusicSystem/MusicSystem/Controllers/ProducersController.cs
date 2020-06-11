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
    public class ProducersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IProducersService producerService;
        private readonly IAlbumsService albumsServicec;

        public ProducersController(ApplicationDbContext context, IProducersService producerService, IAlbumsService albumsServicec)
        {
            _context = context;
            this.producerService = producerService;
            this.albumsServicec = albumsServicec;
        }

        // GET: api/Producers
        [HttpGet]
        public ActionResult<IEnumerable<ProducerDto>> GetProducers()
        {
            return this.producerService.GetAll<ProducerDto>().ToList();
        }

        // GET: api/Producers/5
        [HttpGet("{id}")]
        public ActionResult<ProducerDto> GetProducer(int id)
        {
            var exists = this.producerService.Exists(id);

            if (!exists)
            {
                return this.NotFound();
            }

            var producer = this.producerService.GetById<ProducerDto>(id);

            return producer;
        }

        // PUT: api/Producers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducer(int id, ProducerDto producer)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            producer.Id = id;


            var result = await this.producerService.Update(id, producer);

            if (result == false)
                return this.BadRequest();

            //_context.Entry(song).State = EntityState.Modified;


            return CreatedAtAction("GetProducer", new { id = id }, producer);
        }

        // POST: api/Producers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Producer>> PostProducer(ProducerDto producer)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest();
            }

            var id = await this.producerService.Add(producer);

            return CreatedAtAction("GetProducer", new { id = id }, producer);
        }

        // DELETE: api/Producers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProducerDto>> DeleteProducer(int id)
        {
            var producer = this.producerService.GetById<ProducerDto>(id);
            if (producer == null)
            {
                return this.NotFound();
            }

            await this.albumsServicec.DeleteByProducerId(producer.Id);
            await this.producerService.Delete(producer.Id);

            return producer;
        }

    }
}
