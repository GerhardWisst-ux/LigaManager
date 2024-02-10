using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LigaManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabellenController : ControllerBase
    {
        private readonly ITabelleRepository TabellenRepository;

        public TabellenController(ITabelleRepository TabellenRepository)
        {
            this.TabellenRepository = TabellenRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetTabellen()
        {
            try
            {
                return Ok(await TabellenRepository.GetTabellen());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Lesen der Daten aus der Datenbank");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tabelle>> GetTabelle(int id)
        {
            try
            {
                var result = await TabellenRepository.GetTabelle(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Lesen der Daten aus der Datenbank");
            }
        }

        [HttpPost]
        public async Task<ActionResult<Tabelle>> CreateTabelle(Tabelle Tabelle)
        {
            try
            {
                if (Tabelle == null)
                {
                    return BadRequest();
                }

               
                var createdTabelle = await TabellenRepository.AddTabelle(Tabelle);

                return CreatedAtAction(nameof(GetTabelle), new { id = createdTabelle.Id },
                    createdTabelle);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Lesen der Daten aus der Datenbank");
            }
        }

        [HttpPut()]
        public async Task<ActionResult<Tabelle>> UpdateTabelle(Tabelle Tabelle)
        {
            try
            {
                var TabelleToUpdate = await TabellenRepository.GetTabelle(Tabelle.Id);

                if (TabelleToUpdate == null)
                {
                    return NotFound($"Tabelle mi der Id = {Tabelle.Id} nicht gefunden");
                }

                return await TabellenRepository.UpdateTabelle(Tabelle);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Update der Daten");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Tabelle>> DeleteTabelle(int id)
        {
            try
            {
                var TabelleToDelete = await TabellenRepository.GetTabelle(id);

                if (TabelleToDelete == null)
                {
                    return NotFound($"Tabelle mit der Id = {id} nicht gefunden");
                }

                return await TabellenRepository.DeleteTabelle(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Löschen der Daten");
            }
        }
    }
}
