using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace StadionManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadionController : ControllerBase
    {
        private readonly IStadionRepository StadionRepository;

        public StadionController(IStadionRepository StadionRepository)
        {
            this.StadionRepository = StadionRepository;
        }

    
        [HttpGet]
        public async Task<ActionResult> GetStadien()
        {
            try
            {
                return Ok(await StadionRepository.GetStadien());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Stadion>> GetStadion(int id)
        {
            try
            {
                var result = await StadionRepository.GetStadion(id);

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
        public async Task<ActionResult<Stadion>> CreateStadion(Stadion Stadion)
        {
            try
            {
                if(Stadion == null)
                {
                    return BadRequest();
                } 
              
                var createdStadion = await StadionRepository.AddStadion(Stadion);

                return CreatedAtAction(nameof(GetStadion), new { id = createdStadion.Id },
                    createdStadion);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }           
        }

        [HttpPut()]
        public async Task<ActionResult<Stadion>> UpdateStadion(Stadion Stadion)
        {
            try
            {
                var StadionToUpdate = await StadionRepository.GetStadion(Stadion.Id);

                if(Stadion == null)
                {
                    return NotFound($"Stadion with Id = {Stadion.Id} not found");
                }

                return await StadionRepository.UpdateStadion(Stadion);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data:" + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Stadion>> DeleteStadion(int id)
        {
            try
            {
                var StadionToDelete = await StadionRepository.GetStadion(id);

                if (StadionToDelete == null)
                {
                    return NotFound($"Stadion with Id = {id} not found");
                }

                return await StadionRepository.DeleteStadion(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data:" + ex.Message);
            }
        }
    }
}