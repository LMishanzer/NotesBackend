using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using NotesBackend.Models;
using NotesBackend.Models.Dto;
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
            var userId = User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value;

            return Ok(await _storage.GetUserNotes(userId));
        }

        [HttpGet]
        [Route("/api/v1/[controller]/[action]/{id}")]
        public async Task<IActionResult> GetNote(string id)
        {
            return Ok(await _storage.GetNote(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote([FromBody] NoteDto note)
        {
            try
            {
                var userId = User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value;

                return Ok(await _storage.CreateNote(note, userId));
            }
            catch (NullReferenceException)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNote([FromBody] Note noteUpdate)
        {
            try
            {
                var userId = User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value;
                var note = await _storage.GetNote(noteUpdate.Id);

                if (note?.UserId == userId)
                {
                    await _storage.UpdateNote(noteUpdate);
                    return Ok();
                }

                return BadRequest(new {exception = "Cannot find a note with such ID"});
            }
            catch (Exception)
            {
                return BadRequest(new {exception = "Cannot find a note with such ID"});
            }
        }

        [HttpDelete]
        [Route("/api/v1/[controller]/[action]/{id}")]
        public async Task<IActionResult> DeleteNote(string id)
        {
            try
            {
                var userId = User.Claims.Single(c => c.Type == ClaimTypes.Sid).Value;
                var noteToDelete = await _storage.GetNote(id);

                if (noteToDelete?.UserId == userId)
                {
                    await _storage.DeleteNote(id);
                    return Ok();
                }

                return BadRequest(new {exception = "Cannot find a note with such ID"});
            }
            catch (Exception)
            {
                return BadRequest(new {exception = "Cannot find a note with such ID"});
            }
        }
    }
}
