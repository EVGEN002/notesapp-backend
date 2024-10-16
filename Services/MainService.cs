using Microsoft.EntityFrameworkCore;
using TestApplication.Contexts;
using TestApplication.Models;

namespace TestApplication.Services
{
    public class MainService
    {
        private readonly NotesContext _notesContext;

        public MainService(NotesContext notesContext)
        {
            _notesContext = notesContext;
        }

        public async Task<List<Note>> GetNotes()
        {
            return await _notesContext.notes.ToListAsync();
        }

        public async Task<Note?> GetNote(Guid guid)
        {
            Note? note = await _notesContext.notes.FirstOrDefaultAsync(n => n.guid == guid);

            return note;
        }

        public async Task<Note> CreateNote(Note newNote)
        {
            DateTime createdAt = DateTime.UtcNow;

            newNote.created_at = createdAt;
            newNote.archived = false;

            await _notesContext.notes.AddAsync(newNote);
            await _notesContext.SaveChangesAsync();

            return newNote;
        }

        public async Task<bool> UpdateNote(Note note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note));
            }

            DateTime now = DateTime.UtcNow;
            Note? existingNote = await _notesContext.notes.FirstOrDefaultAsync(n => n.guid == note.guid);

            if (existingNote == null)
            {
                return false;
            }

            existingNote.text = note.text ?? existingNote.text;
            existingNote.status = note.status ?? existingNote.status;
            existingNote.updated_at = now;

            if (note.status == 3)
            {
                existingNote.completed_at = now;
            }

            switch (note.status)
            {
                case 1:
                    existingNote.started_at = now;
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    existingNote.completed_at = now;
                    if (existingNote.started_at != null && existingNote.completed_at != null)
                    {
                        TimeSpan timeDifference = (DateTime)existingNote.completed_at - (DateTime)existingNote.started_at;
                        int minutes = (int)timeDifference.TotalMinutes;

                        existingNote.total_time_spent = minutes;
                    } else
                    {
                        existingNote.started_at = now;
                        existingNote.total_time_spent = 0;
                    }
                    break;
                case 5:
                    existingNote.archived = true;
                    break;
            }

            try
            {
                await _notesContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<bool> ArchiveNote(Guid guid)
        {
            Note? existingNote = await _notesContext.notes.FirstOrDefaultAsync(n => n.guid == guid);

            if (existingNote == null)
            {
                return false;
            }

            existingNote.archived = true;

            try
            {
                await _notesContext.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }


        public async Task<bool> DeleteNote(Guid guid)
        {
            Note? existingNote = await _notesContext.notes.FirstOrDefaultAsync(n => n.guid == guid);

            if (existingNote == null)
            {
                return false;
            }

            try
            {
                _notesContext.notes.Remove(existingNote);
                await _notesContext.SaveChangesAsync();

                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
