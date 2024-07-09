using LigaManagement.Api.Models;
using LigamanagerManagement.Api.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LigaManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository UserRepository;

        public UserController(IUserRepository UserRepository)
        {
            this.UserRepository = UserRepository;
        }

    
        [HttpGet]
        public async Task<ActionResult> GetUser()
        {
            try
            {
                return Ok(await UserRepository.GetUsers());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            try
            {
                var result = await UserRepository.GetUser(id);

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
        public async Task<ActionResult<User>> CreateUser(User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                } 
              
                var createdUser = await UserRepository.CreateUser(user);

                return CreatedAtAction(nameof(CreateUser), new { username = createdUser.Username },
                    createdUser);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database:" + ex.Message);
            }           
        }

        //[HttpPut()]
        //public async Task<ActionResult<User> UpdateTor(User tor)
        //{
        //    try
        //    {
        //        var torToUpdate = await UserRepository.GetTor(tor.Id);

        //        if(ligaToUpdate == null)
        //        {
        //            return NotFound($"Liga with Id = {liga.Id} not found");
        //        }

        //        return await ligaRepository.UpdateLiga(liga);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError,
        //            "Error updating data:" + ex.Message);
        //    }
        //}

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            try
            {
                var userToDelete = await UserRepository.GetUser(id);

                if (userToDelete == null)
                {
                    return NotFound($"user with Id = {id} not found");
                }

                return await UserRepository.DeleteUser(id);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data:" + ex.Message);
            }
        }
    }
}