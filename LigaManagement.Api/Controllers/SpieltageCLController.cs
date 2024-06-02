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
    public class SpieltageCLController : ControllerBase
    {
        private readonly ISpieltageCLRepository PokalergebnisCLSpieltagRepository;

        public SpieltageCLController(ISpieltageCLRepository PokalergebnisCLSpieltagRepository)
        {
            this.PokalergebnisCLSpieltagRepository = PokalergebnisCLSpieltagRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetSpieltage()
        {
            try
            {
                return Ok(await PokalergebnisCLSpieltagRepository.GetSpieltage());
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Lesen der Daten aus der Datenbank:" + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PokalergebnisCLSpieltag>> GetSpieltag(int id)
        {
            try
            {
                var result = await PokalergebnisCLSpieltagRepository.GetSpieltag(id);

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
        public async Task<ActionResult<Spieltag>> CreateSpieltag(PokalergebnisCLSpieltag spieltag)
        {
            try
            {
                if (spieltag == null)
                {
                    return BadRequest();
                }
               
                var createdSpieltag = await PokalergebnisCLSpieltagRepository.AddSpieltag(spieltag);

                return CreatedAtAction(nameof(CreateSpieltag), new { id = createdSpieltag.SpieltagId },
                    createdSpieltag);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Fehler bei der Neuanlage der Daten:" + ex.Message);
            }
        }

        [HttpPut()]        
        public async Task<ActionResult<PokalergebnisCLSpieltag>> UpdateSpieltag(PokalergebnisCLSpieltag Spieltag)
        {
            try
            {
                var VereinToUpdate = await PokalergebnisCLSpieltagRepository.GetSpieltag((int)Spieltag.SpieltagId);

                if (VereinToUpdate == null)
                {
                    return NotFound($"Spieltag mit der Id = {Spieltag.SpieltagId} nicht gefunden");
                }

                return await PokalergebnisCLSpieltagRepository.UpdateSpieltag(Spieltag);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Updaten der Daten:" + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<PokalergebnisCLSpieltag>> DeleteSpieltag(int id)
        {
            try
            {
                var spieltagToDelete = await PokalergebnisCLSpieltagRepository.GetSpieltag(id);

                if (spieltagToDelete == null)
                {
                    return NotFound($"Spieltag mit der Id = {id} nicht gefunden");
                }

                return await PokalergebnisCLSpieltagRepository.DeleteSpieltag(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Löschen der Daten:" + ex.Message);
            }
        }
    }
}
