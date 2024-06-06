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
    public class VereineL3Controller : ControllerBase
    {
        private readonly IVereinRepository VereinRepository;

        public VereineL3Controller(IVereinRepository VereinRepository)
        {
            this.VereinRepository = VereinRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetVereine()
        {
            try
            {
                return Ok(await VereinRepository.GetVereineL3());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Lesen der Daten aus der Datenbank:" + ex.Message);
            }
        }



        [HttpGet("{saison}")]
        public async Task<ActionResult> GetVereineSaison()
        {
            throw new NotImplementedException("VereineL3Controller");
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Verein>> GetVerein(int Id)
        {
            try
            {
                var result = await VereinRepository.GetVereinL3(Id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
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
