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
    public class ToreController : ControllerBase
    {
        private readonly IToreRepository toreRepository;

        public ToreController(IToreRepository toreRepository)
        {
            this.toreRepository = toreRepository;
        }

    
        [HttpGet]
        public async Task<ActionResult> GetTore()
        {
            try
            {
                return Ok(await toreRepository.GetTore());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tore>> GetTor(int id)
        {
            try
            {
                var result = await toreRepository.GetTor(id);

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
        public async Task<ActionResult<Tore>> CreateTor(Tore tor)
        {
            try
            {
                if (tor == null)
                {
                    return BadRequest();
                } 
              
                var createdTor = await toreRepository.CreateTor(tor);

                return CreatedAtAction(nameof(CreateTor), new { id = createdTor.Id }, createdTor);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }           
        }

        //[HttpPut()]
        //public async Task<ActionResult<Tore> UpdateTor(Tore tor)
        //{
        //    try
        //    {
        //        var torToUpdate = await toreRepository.GetTor(tor.Id);

        //        if(ligaToUpdate == null)
        //        {
        //            return NotFound($"Liga with Id = {liga.Id} not found");
        //        }

        //        return await ligaRepository.UpdateLiga(liga);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error updating data:" + ex.Message);
        //    }
        //}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Tore>> DeleteTor(int id)
        {
            try
            {
                var torToDelete = await toreRepository.GetTor(id);

                if (torToDelete == null)
                {
                    return NotFound($"Liga with Id = {id} not found");
                }

                return await toreRepository.DeleteTor(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data:" + ex.Message);
            }
        }
    }
}