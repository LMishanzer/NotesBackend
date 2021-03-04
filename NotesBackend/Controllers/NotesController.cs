using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using NotesBackend.Models;
using NotesBackend.Storage;

namespace NotesBackend.Controllers
{
    [Route("/api/v1/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class NotesController : Controller
    {
        private readonly IStorage _storage;

        public NotesController(IStorage storage)
        {
            _storage = storage;
        }   

        [HttpGet]
        public async Task<IActionResult> GetAllNotes()
        {
            return Ok(await _storage.GetAllNotes());
        }

        [HttpGet]
        [Route("/api/v1/[controller]/[action]/{id}")]
        public async Task<IActionResult> GetNote(string id)
        {
            return Ok(await _storage.GetNote(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] Note note)
        {
            try
            {
                return Ok(await _storage.CreateNote(note));
            }
            catch (NullReferenceException)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNote([FromBody] Note note)
        {
            await _storage.UpdateNote(note);
            return Ok();
        }

        [HttpDelete]
        [Route("/api/v1/[controller]/[action]/{id}")]
        public async Task<IActionResult> DeleteNote(string id)
        {
            await _storage.DeleteNote(id);
            return Ok();
        }
    }
}
