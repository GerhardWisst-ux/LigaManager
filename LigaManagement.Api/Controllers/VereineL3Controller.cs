using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace LigaManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VereineL3Controller : ControllerBase
    {
        private readonly IVereinRepository VereinRepository;

        public VereineL3Controller(IVereinRepository VereinRepository)
        {
            this.VereinRepository = VereinRepository;
        }        

        [HttpGet]
        public async Task<ActionResult<List<Verein>>> GetVereine()
        {
            try
            {
                var vereine = await VereinRepository.GetVereineL3();

                if (vereine == null || !vereine.Any())
                {
                    return NotFound("Es wurden keine Vereine gefunden.");
                }

                return Ok(vereine);
            }
            catch (Exception ex)
            {
                // Optional: Logging hinzufügen, z. B. mit einem Logger-Service
                // _logger.LogError(ex, "Fehler beim Abrufen der Vereine");
                
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ein unerwarteter Fehler ist aufgetreten. Bitte versuchen Sie es später erneut.");
            }
        }



        [HttpGet("{saison}")]
        public async Task<ActionResult> GetVereineSaison()
        {
            throw new NotImplementedException("VereineL3Controller");
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Verein>> GetVerein(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Die angegebene ID ist ungültig.");
            }

            try
            {
                var result = await VereinRepository.GetVereinL3(id);

                if (result == null)
                {
                    return NotFound($"Kein Verein mit der ID {id} gefunden.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Optional: Logging hinzufügen, z. B. mit einem Logger-Service
                // _logger.LogError(ex, "Fehler beim Abrufen des Vereins mit ID {id}", id);

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ein unerwarteter Fehler ist aufgetreten. Bitte versuchen Sie es später erneut.");
            }
        }


        [HttpPost]
        public async Task<ActionResult<Verein>> CreateVerein(Verein Verein)
        {
            throw new NotImplementedException("VereineL3Controller");
        }

        [HttpPut()]
        public async Task<ActionResult<Verein>> UpdateVerein(Verein Verein)
        {
            throw new NotImplementedException("VereineL3Controller");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Verein>> DeleteVerein(int Id)
        {
            throw new NotImplementedException("VereineL3Controller");
        }
    }
}
