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
    public class VereineCLController : ControllerBase
    {
        private readonly IVereinRepository VereinRepository;

        public VereineCLController(IVereinRepository VereinRepository)
        {
            this.VereinRepository = VereinRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetVereine()
        {
            try
            {
                return Ok(await VereinRepository.GetVereineCL());
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
           throw new NotImplementedException("vereineCLController");
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Verein>> GetVerein(int Id)
        {
            try
            {
                var result = await VereinRepository.GetVereinCL(Id);

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
        public async Task<ActionResult<Verein>> CreateVerein(VereinAUS Verein)
        {
            throw new NotImplementedException("vereineCLController");
        }

        [HttpPut()]
        public async Task<ActionResult<VereinAUS>> UpdateVerein(VereinAUS Verein)
        {
            throw new NotImplementedException("vereineCLController");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<VereinAUS>> DeleteVerein(int Id)
        {
            throw new NotImplementedException("vereineCLController");
        }
    }
}
