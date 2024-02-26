using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace SpielerManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpielerSpieltagController : ControllerBase
    {
        private readonly ISpielerSpieltagRepository SpielerSpieltagRepository;

        public SpielerSpieltagController(ISpielerSpieltagRepository SpielerSpieltagRepository)
        {
            this.SpielerSpieltagRepository = SpielerSpieltagRepository;
        }

    
        [HttpGet]
        public async Task<ActionResult> GetSpieler()
        {
            try
            {
                return Ok(await SpielerSpieltagRepository.GetAllSpieler());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SpielerSpieltag>> GetSpieler(int id)
        {
            try
            {
                var result = await SpielerSpieltagRepository.GetSpieler(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<SpielerSpieltag>> CreateSpieler(SpielerSpieltag spieler)
        {
            try
            {
                if(spieler == null)
                {
                    return BadRequest();
                } 
              
                var createdSpieler = await SpielerSpieltagRepository.AddSpieler(spieler);

                return CreatedAtAction(nameof(GetSpieler), new { id = createdSpieler.Id },
                    createdSpieler);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }           
        }

        [HttpPut()]
        public async Task<ActionResult<SpielerSpieltag>> UpdateSpieler(SpielerSpieltag spieler)
        {
            try
            {
                var SpielerToUpdate = await SpielerSpieltagRepository.GetSpieler(spieler.Id);

                if(SpielerToUpdate == null)
                {
                    return NotFound($"Spieler with Id = {spieler.Id} not found");
                }

                return await SpielerSpieltagRepository.UpdateSpieler(spieler);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data:" + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<SpielerSpieltag>> DeleteSpieler(SpielerSpieltag spieler)
        {
            try
            {
                var SpielerToDelete = await SpielerSpieltagRepository.GetSpieler(spieler.Id);

                if (SpielerToDelete == null)
                {
                    return NotFound($"Spieler with Id = {spieler.Id} not found");
                }

                return await SpielerSpieltagRepository.DeleteSpieler(spieler);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data:" + ex.Message);
            }
        }
    }
}