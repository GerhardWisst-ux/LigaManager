﻿using LigaManagement.Models;
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
    public class SpieltageBEController : ControllerBase
    {
        private readonly ISpieltageBERepository SpieltagBERepository;

        public SpieltageBEController(ISpieltageBERepository SpieltagBERepository)
        {
            this.SpieltagBERepository = SpieltagBERepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetSpieltage()
        {
            try
            {
                return Ok(await SpieltagBERepository.GetSpieltage());
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Lesen der Daten aus der Datenbank:" + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Spieltag>> GetSpieltag(int id)
        {
            try
            {
                var result = await SpieltagBERepository.GetSpieltag(id);

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
        public async Task<ActionResult<Spieltag>> CreateSpieltag(Spieltag spieltag)
        {
            try
            {
                if (spieltag == null)
                {
                    return BadRequest();
                }
               
                var createdSpieltag = await SpieltagBERepository.AddSpieltag(spieltag);

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
        public async Task<ActionResult<Spieltag>> UpdateSpieltag(Spieltag Spieltag)
        {
            try
            {
                var VereinToUpdate = await SpieltagBERepository.GetSpieltag((int)Spieltag.SpieltagId);

                if (VereinToUpdate == null)
                {
                    return NotFound($"Spieltag mit der Id = {Spieltag.SpieltagId} nicht gefunden");
                }

                return await SpieltagBERepository.UpdateSpieltag(Spieltag);
            }
            catch (Exception ex)
            {
                Debug.Print(ex.StackTrace);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Updaten der Daten:" + ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Spieltag>> DeleteSpieltag(int id)
        {
            try
            {
                var spieltagToDelete = await SpieltagBERepository.GetSpieltag(id);

                if (spieltagToDelete == null)
                {
                    return NotFound($"Spieltag mit der Id = {id} nicht gefunden");
                }

                return await SpieltagBERepository.DeleteSpieltag(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Fehler beim Löschen der Daten:" + ex.Message);
            }
        }
    }
}
