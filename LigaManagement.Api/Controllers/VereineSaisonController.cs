using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using LigaManagerManagement.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace LigaManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class VereineSaisonController : ControllerBase
    {
        private readonly IVereineSaisonRepository VereineSaisonRepository;

        public VereineSaisonController(IVereineSaisonRepository VereineSaisonRepository)
        {
            this.VereineSaisonRepository = VereineSaisonRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetVereine()
        {
            try
            {
                return Ok(await VereineSaisonRepository.GetVereineSaison());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Lesen der Daten aus der Datenbank:" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<VereineSaison>> CreateVereineSaison(List<VereineSaison> vereineSaison)
        {
            try
            {
                if (vereineSaison == null)
                {
                    return BadRequest();
                }
                               
                var createdVereine = await VereineSaisonRepository.AddVereineSaison(vereineSaison);

                return CreatedAtAction(nameof(CreateVereineSaison), new { Saisonid = vereineSaison[0].SaisonID },
                   createdVereine);               
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }

        [HttpDelete("{saisonid:int}")]
        public async Task<ActionResult<VereineSaison>> DeleteVereineSaison(int saisonid)
        {
            try
            {                

                return await VereineSaisonRepository.DeleteVereineSaison(saisonid);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data:" + ex.Message);
            }
        }
    }
}
