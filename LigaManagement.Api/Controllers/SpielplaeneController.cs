using LigaManagement.Models;
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
    public class SpielplaeneController : ControllerBase
    {
        private readonly ISpielplaeneRepository _spielplanRepository;
        private readonly ILogger<SpielplaeneController> _logger;

        public SpielplaeneController(ISpielplaeneRepository spielplanRepository, ILogger<SpielplaeneController> logger)
        {
            _spielplanRepository = spielplanRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetSpielplaene()
        {
            try
            {
                _logger.LogInformation("Fetching all Spielplaene.");
                var spielplaene = await _spielplanRepository.GetSpielplaene();
                return Ok(spielplaene);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching Spielplaene.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Fehler beim Abrufen der Spielpläne.");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetSpielplan(int id)
        {
            try
            {
                _logger.LogInformation($"Fetching Spielplan with ID {id}.");
                var spielplan = await _spielplanRepository.GetSpielplan(id);

                if (spielplan == null)
                {
                    _logger.LogWarning($"Spielplan with ID {id} not found.");
                    return NotFound($"Spielplan mit der ID {id} wurde nicht gefunden.");
                }

                return Ok(spielplan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error fetching Spielplan with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Fehler beim Abrufen des Spielplans.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSpielplan([FromBody] Spielplan spielplan)
        {
            try
            {
                if (spielplan == null)
                {
                    _logger.LogWarning("Invalid Spielplan object received.");
                    return BadRequest("Ungültige Daten.");
                }

                var createdSpielplan = await _spielplanRepository.AddSpielplan(spielplan);
                return CreatedAtAction(nameof(GetSpielplan), new { id = createdSpielplan.SpieltagId }, createdSpielplan);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Spielplan.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Fehler beim Erstellen des Spielplans.");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSpielplan(int id, [FromBody] Spielplan spielplan)
        {
            try
            {
                if (spielplan == null || id != spielplan.SpieltagId)
                {
                    _logger.LogWarning("Invalid Spielplan object or ID mismatch.");
                    return BadRequest("Ungültige Daten.");
                }

                var existingSpielplan = await _spielplanRepository.GetSpielplan(id);
                if (existingSpielplan == null)
                {
                    _logger.LogWarning($"Spielplan with ID {id} not found.");
                    return NotFound($"Spielplan mit der ID {id} wurde nicht gefunden.");
                }

                await _spielplanRepository.UpdateSpielplan(spielplan);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating Spielplan with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Fehler beim Aktualisieren des Spielplans.");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSpielplan(int id)
        {
            try
            {
                _logger.LogInformation($"Deleting Spielplan with ID {id}.");
                var existingSpielplan = await _spielplanRepository.GetSpielplan(id);

                if (existingSpielplan == null)
                {
                    _logger.LogWarning($"Spielplan with ID {id} not found.");
                    return NotFound($"Spielplan mit der ID {id} wurde nicht gefunden.");
                }

                await _spielplanRepository.DeleteSpielplan(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting Spielplan with ID {id}.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Fehler beim Löschen des Spielplans.");
            }
        }
    }
}