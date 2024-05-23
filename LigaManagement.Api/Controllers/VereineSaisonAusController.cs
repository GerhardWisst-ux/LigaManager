using LigaManagement.Models;
using Ligamanager.Components;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LigaManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VereineSaisonAusController : ControllerBase
    {
        private readonly IVereineSaisonAusRepository VereineSaisonAusRepository;

        public VereineSaisonAusController(IVereineSaisonAusRepository VereineSaisonAusRepository)
        {
            this.VereineSaisonAusRepository = VereineSaisonAusRepository;
        }

        [HttpGet("{saisonid:int}")]
        public async Task<ActionResult> GetVereine(int saisonid)
        {
            try
            {
                return Ok(await VereineSaisonAusRepository.GetVereineSaison(saisonid));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Lesen der Daten aus der Datenbank:" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<VereineSaisonAus>> CreateVereineSaison(List<VereineSaisonAus> vereineSaison)
        {
            try
            {
                if (vereineSaison == null)
                {
                    return BadRequest();
                }
                               
                var createdVereine = await VereineSaisonAusRepository.AddVereineSaison(Globals.LigaID, Globals.SaisonID);

                return CreatedAtAction(nameof(CreateVereineSaison), new { id = 87777 },
                   createdVereine);               
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }
    }
}
