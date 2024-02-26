using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LigaManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VereineSaisonController : ControllerBase
    {
        private readonly IVereinRepository VereinRepository;

        public VereineSaisonController(IVereinRepository VereinRepository)
        {
            this.VereinRepository = VereinRepository;
        }        

        [HttpGet]
        public async Task<ActionResult> GetVereineSaison()
        {
            try
            {
                return Ok(await VereinRepository.GetVereineSaison());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Lesen der Daten aus der Datenbank:" + ex.Message);
            }
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Verein>> GetVerein(int Id)
        {
            try
            {
                var result = await VereinRepository.GetVerein(Id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Verein>> CreateVerein(Verein Verein)
        {
            try
            {
                if (Verein == null)
                {
                    return BadRequest();
                }
               
                var createdVerein = await VereinRepository.AddVerein(Verein);

                return CreatedAtAction(nameof(GetVerein), new { id = createdVerein.Id },
                    createdVerein);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpPut()]
        public async Task<ActionResult<Verein>> UpdateVerein(Verein Verein)
        {
            try
            {
                var VereinToUpdate = await VereinRepository.GetVerein(Verein.Id);

                if (VereinToUpdate == null)
                {
                    return NotFound($"Verein mit der Id = {VereinToUpdate.Id} nicht gefunden");
                }
                
                return await VereinRepository.UpdateVerein(Verein);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Update der Daten:" + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Verein>> DeleteVerein(int Id)
        {
            try
            {
                var VereinToDelete = await VereinRepository.GetVerein(Id);

                if (VereinToDelete == null)
                {
                    return NotFound($"Verein mit der Id = {Id} nicht gefunden");
                }

                return await VereinRepository.DeleteVerein(Id);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Löschen der Daten:" + ex.Message);
            }
        }
    }
}
