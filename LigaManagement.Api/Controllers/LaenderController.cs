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
    public class LaenderController : ControllerBase
    {
        private readonly ILaenderRepository laenderRepository;

        public LaenderController(ILaenderRepository laenderRepository)
        {
            this.laenderRepository = laenderRepository;
        }
                
        public async Task<ActionResult> GetLaender()
        {
            try
            {
                return Ok(await laenderRepository.GetLaender());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }
        }
                
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Land>> GetLand(int id)
        {
            try
            {
                var result = await laenderRepository.GetLand(id);

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
        public async Task<ActionResult<Land>> CreateLand(Land land)
        {
            try
            {
                if(land == null)
                {
                    return BadRequest();
                } 
              
                var createdLiga = await laenderRepository.AddLand(land);

                return CreatedAtAction(nameof(CreateLand), new { id = createdLiga.Id },
                    createdLiga);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }           
        }

        [HttpPut()]
        public async Task<ActionResult<Land>> UpdateLiga(Land land)
        {
            try
            {
                var ligaToUpdate = await laenderRepository.GetLand(land.Id);

                if(ligaToUpdate == null)
                {
                    return NotFound($"Land with Id = {land.Id} not found");
                }

                return await laenderRepository.UpdateLand(land);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data:" + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Land>> Deleteland(int id)
        {
            try
            {
                var ligaToDelete = await laenderRepository.GetLand(id);

                if (ligaToDelete == null)
                {
                    return NotFound($"Land with Id = {id} not found");
                }

                return await laenderRepository.DeleteLand(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data:" + ex.Message);
            }
        }
    }
}