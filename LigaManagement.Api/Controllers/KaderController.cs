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
    public class KaderController : ControllerBase
    {
        private readonly IKaderRepository SpielerRepository;

        public KaderController(IKaderRepository SpielerRepository)
        {
            this.SpielerRepository = SpielerRepository;
        }

    
        [HttpGet]
        public async Task<ActionResult> GetSpieler()
        {
            try
            {
                return Ok(await SpielerRepository.GetAllSpieler());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Kader>> GetSpieler(int id)
        {
            try
            {
                var result = await SpielerRepository.GetSpieler(id);

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
        public async Task<ActionResult<Kader>> CreateSpieler(Kader spieler)
        {
            try
            {
                if(spieler == null)
                {
                    return BadRequest();
                } 
              
                var createdSpieler = await SpielerRepository.AddSpieler(spieler);

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
        public async Task<ActionResult<Kader>> UpdateSpieler(Kader spieler)
        {
            try
            {
                var SpielerToUpdate = await SpielerRepository.GetSpieler(spieler.Id);

                if(SpielerToUpdate == null)
                {
                    return NotFound($"Spieler with Id = {spieler.Id} not found");
                }

                return await SpielerRepository.UpdateSpieler(spieler);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data:" + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Kader>> DeleteSpieler(int id)
        {
            try
            {
                var SpielerToDelete = await SpielerRepository.GetSpieler(id);

                if (SpielerToDelete == null)
                {
                    return NotFound($"Spieler with Id = {id} not found");
                }

                return await SpielerRepository.DeleteSpieler(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data:" + ex.Message);
            }
        }
    }
}