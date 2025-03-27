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
    public class SpielplaeneController : ControllerBase
    {
        private readonly ISpielplaeneRepository SpielplanRepository;

        public SpielplaeneController(ISpielplaeneRepository SpielplanRepository)
        {
            this.SpielplanRepository = SpielplanRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetSpielplaene()
        {
            try
            {
                return Ok(await SpielplanRepository.GetSpielplaene());
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Lesen der Daten aus der Datenbank:" + ex.Message);
            }
        }
               

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Spielplan>> GetSpielplan(int id)
        {
            try
            {
                var result = await SpielplanRepository.GetSpielplan(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Lesen der Daten aus der Datenbank:" + ex.Message);
            }
        }

       
        [HttpPost]
        public async Task<ActionResult<Spielplan>> CreateSpielplan(Spielplan Spielplan)
        {
            try
            {
                if (Spielplan == null)
                {
                    return BadRequest();
                }
               
                var createdSpielplan = await SpielplanRepository.AddSpielplan(Spielplan);

                return CreatedAtAction(nameof(CreateSpielplan), new { id = createdSpielplan.SpieltagId },
                    createdSpielplan);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Fehler bei der Neuanlage der Daten:" + ex.Message);
            }
        }

        [HttpPut()]        
        public async Task<ActionResult<Spielplan>> UpdateSpielplan(Spielplan Spielplan)
        {
            try
            {
                var VereinToUpdate = await SpielplanRepository.GetSpielplan((int)Spielplan.SpieltagId);

                if (VereinToUpdate == null)
                {
                    return NotFound($"Spielplan mit der Id = {Spielplan.SpieltagId} nicht gefunden");
                }

                return await SpielplanRepository.UpdateSpielplan(Spielplan);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Updaten der Daten:" + ex.Message);
            }
        }

        [HttpDelete("{id:int}/{liganummer:int}")]        
        public async Task<ActionResult<Spielplan>> DeleteSpielplan(int id, int liganummer)
        {
            try
            {
                var SpielplanToDelete = await SpielplanRepository.GetSpielplan(id);

                if (SpielplanToDelete == null)
                {
                    return NotFound($"Spielplan mit der Id = {id} nicht gefunden");
                }

                if (liganummer < 3)
                    return await SpielplanRepository.DeleteSpielplan(id);
                if (liganummer== 3)
                    return await SpielplanRepository.DeleteSpielplanL3(id);
                else
                    return await SpielplanRepository.DeleteSpielplan(id);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Löschen der Daten:" + ex.Message);
            }
        }
    }
}
