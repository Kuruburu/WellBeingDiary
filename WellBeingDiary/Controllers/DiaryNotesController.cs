using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WellBeingDiary.Data;
using WellBeingDiary.Dtos.DiaryNote;
using WellBeingDiary.Entities;
using WellBeingDiary.Mappers;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var diaryNotes = await _context.DiaryNotes.ToListAsync();

            var diaryNoteDto = diaryNotes.Select(d => d.ToDiaryNoteDto());

            return Ok(diaryNoteDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var diaryNote = await _context.DiaryNotes.FindAsync(id);

            if(diaryNote == null)
            {
                return NotFound();
            }

            return Ok(diaryNote.ToDiaryNoteDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDiaryNoteRequestDto diaryNoteDto)
        {
            var diaryNoteModel =  diaryNoteDto.ToDiaryNoteFromCreateDto();
            await _context.DiaryNotes.AddAsync(diaryNoteModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = diaryNoteModel.Id }, diaryNoteModel.ToDiaryNoteDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDiaryNoteRequestDto updateDto)
        {
            var diaryNoteModel = await _context.DiaryNotes.FirstOrDefaultAsync(x => x.Id == id);

            if(diaryNoteModel == null)
            {
                return NotFound();
            }

            diaryNoteModel.Title = updateDto.Title;
            diaryNoteModel.Text = updateDto.Text;
            diaryNoteModel.Rating = updateDto.Rating;
            diaryNoteModel.IsPublic = updateDto.IsPublic;

            await _context.SaveChangesAsync();

            return Ok(diaryNoteModel.ToDiaryNoteDto());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var diaryNoteModel = await _context.DiaryNotes.FirstOrDefaultAsync(x => x.Id ==id);
            
            if(diaryNoteModel == null)
            {
                return NotFound();
            }

            _context.DiaryNotes.Remove(diaryNoteModel);
            await _context.SaveChangesAsync();
            
            return NoContent();
        }

    
    }
}
