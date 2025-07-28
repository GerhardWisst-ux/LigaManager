using LigaManagement.Models;
using Ligamanager.Components;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LigaManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpieltageController : ControllerBase
    {
        private readonly ISpieltageRepository _spieltagRepository;
        private readonly ILogger<SpieltageController> _logger;

        public SpieltageController(ISpieltageRepository spieltagRepository, ILogger<SpieltageController> logger)
        {
            _spieltagRepository = spieltagRepository;
            _logger = logger;
        }

        [HttpGet("GetSpieltage")]
        public async Task<ActionResult> GetSpieltage()
        {
            try
            {
                var spieltage = await _spieltagRepository.GetSpieltage(); // Nutzung von AsNoTracking
                return Ok(spieltage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Abrufen der Spieltage");
                return StatusCode(StatusCodes.Status500InternalServerError, "Fehler beim Lesen der Daten aus der Datenbank.");
            }
        }

        [HttpGet("GetSpieltageCount")]
        public async Task<ActionResult> GetSpieltageCount()
        {
            try
            {
                var spieltageCount = await _spieltagRepository.GetSpieltageCount(); // Nutzung von AsNoTracking
                return Ok(spieltageCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Abrufen der Spieltage");
                return StatusCode(StatusCodes.Status500InternalServerError, "Fehler beim Lesen der Daten aus der Datenbank.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Spieltag>> GetSpieltag(int id)
        {
            try
            {
                var result = await _spieltagRepository.GetSpieltag(id); // Nutzung von AsNoTracking
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Abrufen des Spieltags");
                return StatusCode(StatusCodes.Status500InternalServerError, "Fehler beim Lesen der Daten aus der Datenbank.");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Spieltag>> CreateSpieltag(Spieltag spieltag)
        {
            Spieltag createdSpieltag;
            if (spieltag == null) return BadRequest();
            try
            {
                if (spieltag.LigaID < 3)
                    createdSpieltag = await _spieltagRepository.AddSpieltag(spieltag);
                else                
                    createdSpieltag = await _spieltagRepository.AddSpieltagL3(spieltag);
                
                return CreatedAtAction(nameof(GetSpieltag), new { id = createdSpieltag.SpieltagId }, createdSpieltag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler bei der Neuanlage des Spieltags");
                return StatusCode(StatusCodes.Status500InternalServerError, "Fehler bei der Neuanlage der Daten.");
            }
        }

        [HttpPut]
        public async Task<ActionResult<Spieltag>> UpdateSpieltag(Spieltag spieltag)
        {
            if (spieltag == null) return BadRequest("Ungültige Daten.");
            try
            {
                return await _spieltagRepository.UpdateSpieltag(spieltag);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Updaten des Spieltags");
                return StatusCode(StatusCodes.Status500InternalServerError, "Fehler beim Updaten der Daten.");
            }
        }

        [HttpDelete("{id:int}/{liganummer:int}")]
        public async Task<ActionResult<Spieltag>> DeleteSpieltag(int id, int liganummer)
        {
            try
            {
                return liganummer == 3
                    ? await _spieltagRepository.DeleteSpieltagL3(id)
                    : await _spieltagRepository.DeleteSpieltag(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fehler beim Löschen des Spieltags");
                return StatusCode(StatusCodes.Status500InternalServerError, "Fehler beim Löschen der Daten.");
            }
        }
    }
}
