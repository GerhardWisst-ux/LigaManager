using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LigaManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LigenController : ControllerBase
    {
        private readonly ILigaRepository ligaRepository;

        public LigenController(ILigaRepository ligaRepository)
        {
            this.ligaRepository = ligaRepository;
        }

    
        [HttpGet]
        public async Task<ActionResult> GetLigen()
        {
            try
            {
                return Ok(await ligaRepository.GetLigen());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Liga>> GetLiga(int id)
        {
            try
            {
                var result = await ligaRepository.GetLiga(id);

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
        public async Task<ActionResult<Liga>> CreateLiga(Liga liga)
        {
            try
            {
                if(liga == null)
                {
                    return BadRequest();
                } 
              
                var createdLiga = await ligaRepository.AddLiga(liga);

                return CreatedAtAction(nameof(GetLiga), new { id = createdLiga.Id },
                    createdLiga);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }           
        }

        [HttpPut()]
        public async Task<ActionResult<Liga>> UpdateLiga(Liga liga)
        {
            try
            {
                var ligaToUpdate = await ligaRepository.GetLiga(liga.Id);

                if(ligaToUpdate == null)
                {
                    return NotFound($"Liga with Id = {liga.Id} not found");
                }

                return await ligaRepository.UpdateLiga(liga);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data:" + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Liga>> DeleteLiga(int id)
        {
            try
            {
                var ligaToDelete = await ligaRepository.GetLiga(id);

                if (ligaToDelete == null)
                {
                    return NotFound($"Liga with Id = {id} not found");
                }

                return await ligaRepository.DeleteLiga(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data:" + ex.Message);
            }
        }
    }
}