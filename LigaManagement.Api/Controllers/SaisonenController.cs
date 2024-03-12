using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Api.Models;
using LigaManagerManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace LigaManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaisonenController : ControllerBase
    {
        private readonly ISaisonRepository SaisonRepository;

        public SaisonenController(ISaisonRepository SaisonRepository)
        {
            this.SaisonRepository = SaisonRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetSaisonen()
        {
            try
            {
                return Ok(await SaisonRepository.GetSaisonen());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Lesen der Daten aus der Datenbank");
            }
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Saison>> GetSaison(int Id)
        {
            try
            {
                var result = await SaisonRepository.GetSaison(Id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   ex.Message + "Fehler beim Lesen der Daten aus der Datenbank");
            }
        }               
        
        [HttpPost]
        public async Task<ActionResult<Saison>> CreateSaison(Saison Saison)
        {
            try
            {
                if (Saison == null)
                {
                    return BadRequest();
                }


                var createdSaison = await SaisonRepository.AddSaison(Saison);

                return CreatedAtAction(nameof(GetSaison), new { id = createdSaison.SaisonID },
                    createdSaison);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Lesen der Daten aus der Datenbank");
            }
        }

        [HttpPut()]
        public async Task<ActionResult<Saison>> UpdateSaison(Saison Saison)
        {
            try
            {
                var VereinToUpdate = await SaisonRepository.GetSaison(Saison.SaisonID);

                if (VereinToUpdate == null)
                {
                    return NotFound($"Saison mi der Id = {Saison.SaisonID} nicht gefunden");
                }

                return await SaisonRepository.UpdateSaison(Saison);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Update der Daten");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Saison>> DeleteSaison(int id)
        {
            try
            {
                var VereinToDelete = await SaisonRepository.GetSaison(id);

                if (VereinToDelete == null)
                {
                    return NotFound($"Saison mit der Id = {id} nicht gefunden");
                }

                return await SaisonRepository.DeleteSaison(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Löschen der Daten");
            }
        }
    }
}
