using LigaManagement.Models;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace InfoTextManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InfoTexteController : ControllerBase
    {
        private readonly IInfoTexteRepository InfoTexteRepository;

        public InfoTexteController(IInfoTexteRepository InfoTexteRepository)
        {
            this.InfoTexteRepository = InfoTexteRepository;
        }

    
        [HttpGet]
        public async Task<ActionResult> GetInfoTexte()
        {
            try
            {
                return Ok(await InfoTexteRepository.GetInfoTexte());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<InfoText>> GetInfoText(int id)
        {
            try
            {
                var result = await InfoTexteRepository.GetInfoText(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<InfoText>> CreateInfoText(InfoText InfoText)
        {
            try
            {
                if(InfoText == null)
                {
                    return BadRequest();
                } 
              
                var createdInfoText = await InfoTexteRepository.AddInfoText(InfoText);

                return CreatedAtAction(nameof(GetInfoText), new { id = createdInfoText.Id },
                    createdInfoText);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }           
        }

        [HttpPut()]
        public async Task<ActionResult<InfoText>> UpdateInfoText(InfoText InfoText)
        {
            try
            {
                var InfoTextToUpdate = await InfoTexteRepository.GetInfoText(InfoText.Id);

                if(InfoText == null)
                {
                    return NotFound($"InfoText with Id = {InfoText.Id} not found");
                }

                return await InfoTexteRepository.UpdateInfoText(InfoText);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data:" + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<InfoText>> DeleteInfoText(int id)
        {
            try
            {
                var InfoTextToDelete = await InfoTexteRepository.GetInfoText(id);

                if (InfoTextToDelete == null)
                {
                    return NotFound($"InfoText with Id = {id} not found");
                }

                return await InfoTexteRepository.DeleteInfoText(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data:" + ex.Message);
            }
        }
    }
}