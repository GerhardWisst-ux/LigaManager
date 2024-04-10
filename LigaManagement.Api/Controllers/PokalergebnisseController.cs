using LigaManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using ToremanagerManagement.Api.Models.Repository;

namespace LigaManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokalergebnisseController : ControllerBase
    {
        private readonly IPokalergebnisseRepository pokalergebnisseRepository;

        public PokalergebnisseController(IPokalergebnisseRepository pokalergebnisseRepository)
        {
            this.pokalergebnisseRepository = pokalergebnisseRepository;
        }

    
        [HttpGet]
        public async Task<ActionResult> GetPokalergebnisse()
        {
            try
            {
                return Ok(await pokalergebnisseRepository.GetPokalergebnisse());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PokalergebnisSpieltag>> GetPokalergebnis(int id)
        {
            try
            {
                var result = await pokalergebnisseRepository.GetPokalergebnis(id);

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
        public async Task<ActionResult<PokalergebnisSpieltag>> CreatePokalergebnis(PokalergebnisSpieltag pokalergebnis)
        {
            try
            {
                if(pokalergebnis == null)
                {
                    return BadRequest();
                } 
              
                var createdLiga = await pokalergebnisseRepository.CreatePokalergebnis(pokalergebnis);

                return CreatedAtAction(nameof(CreatePokalergebnis), new { id = pokalergebnis.SpieltagId },
                    createdLiga);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }           
        }

      
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PokalergebnisSpieltag>> DeletePokalergebnis(int id)
        {
            try
            {
                var torToDelete = await pokalergebnisseRepository.DeletePokalergebnis(id);

                if (torToDelete == null)
                {
                    return NotFound($"Liga with Id = {id} not found");
                }

                return await pokalergebnisseRepository.DeletePokalergebnis(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data:" + ex.Message);
            }
        }
    }
}