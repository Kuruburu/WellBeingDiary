using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellBeingDiary.Data;
using WellBeingDiary.Entities;

namespace WellBeingDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryNotesController : ControllerBase
    {
        private readonly DiaryDbContext _context;

        public DiaryNotesController(DiaryDbContext context)
        {
            _context = context;
        }

        // GET: api/DiaryNotes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiaryNote>>> GetDiaryNotes()
        {
            return await _context.DiaryNotes.ToListAsync();
        }

        // GET: api/DiaryNotes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiaryNote>> GetDiaryNote(int id)
        {
            var diaryNote = await _context.DiaryNotes.FindAsync(id);

            if (diaryNote == null)
            {
                return NotFound();
            }

            return diaryNote;
        }

        // PUT: api/DiaryNotes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiaryNote(int id, DiaryNote diaryNote)
        {
            if (id != diaryNote.Id)
            {
                return BadRequest();
            }

            _context.Entry(diaryNote).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiaryNoteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DiaryNotes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DiaryNote>> PostDiaryNote(DiaryNote diaryNote)
        {
            _context.DiaryNotes.Add(diaryNote);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiaryNote", new { id = diaryNote.Id }, diaryNote);
        }

        // DELETE: api/DiaryNotes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiaryNote(int id)
        {
            var diaryNote = await _context.DiaryNotes.FindAsync(id);
            if (diaryNote == null)
            {
                return NotFound();
            }

            _context.DiaryNotes.Remove(diaryNote);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DiaryNoteExists(int id)
        {
            return _context.DiaryNotes.Any(e => e.Id == id);
        }
    }
}
