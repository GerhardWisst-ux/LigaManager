using LigaManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ToremanagerManagement.Api.Models.Repository;

namespace LigaManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EinstellungenController : ControllerBase
    {
        private readonly IEinstellungenRepository einstellungenrepository;

        public EinstellungenController(IEinstellungenRepository einstellungenRepository)
        {
            this.einstellungenrepository = einstellungenRepository;
        }


        [HttpGet]
        public async Task<ActionResult> GetEinstellungen()
        {
            try
            {
                return Ok(await einstellungenrepository.GetEinstellungen());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }
        }

        [HttpPut()]
        public async Task<ActionResult<EinstellungenLM>> UpdateEinstellungen(EinstellungenLM einstellungen)
        {
            try
            {
                var einstellungenToUpdate = await einstellungenrepository.GetEinstellungen();

                if (einstellungenToUpdate == null)
                {
                    return NotFound($"Einstellungen nicht gefunden");
                }

                return await einstellungenrepository.UpdateEinstellungen(einstellungen);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Updaten der Daten:" + ex.Message);
            }
        }
    }
}