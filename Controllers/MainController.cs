using Microsoft.AspNetCore.Mvc;
using NotesAPI.DTOs;
using NotesAPI.Models;
using NotesAPI.Services;

namespace NotesAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly MainService _mainService;

        public MainController(MainService mainService)
        {
            _mainService = mainService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetNotes()
        {
            List<Note> notes = await _mainService.GetNotes();

            return Ok(notes);
        }

        [HttpGet]
        public async Task<IActionResult> GetNote(Guid guid)
        {
            Note? note = await _mainService.GetNote(guid);

            if (note == null)
            {
                return NotFound();
            }

            return Ok(note);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(CreateNoteDTO newNote)
        {
            Note note = new Note { text = newNote.text, status = newNote.status };

            Note createdNote = await _mainService.CreateNote(note);

            return CreatedAtAction(nameof(_mainService.CreateNote), createdNote);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateNote(EditNoteDTO editingNote)
        {
            Note note = new Note { guid = editingNote.guid, text = editingNote.text, status = editingNote.status };

            var isUpdated = await _mainService.UpdateNote(note);

            if (isUpdated)
            {
                return Ok();
            } else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteNote(Guid guid)
        {
            var isDeleted = await _mainService.DeleteNote(guid);

            if (isDeleted)
            {
                return Ok();
            } else
            {
                return NotFound();
            }
        }
    }
}
