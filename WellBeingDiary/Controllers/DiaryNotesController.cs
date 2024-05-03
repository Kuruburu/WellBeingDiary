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
        public IActionResult GetAll()
        {
            var diaryNotes = _context.DiaryNotes.ToList()
             .Select(d => d.ToDiaryNoteDto());
            return Ok(diaryNotes);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var diaryNote = _context.DiaryNotes.Find(id);

            if(diaryNote == null)
            {
                return NotFound();
            }
            return Ok(diaryNote.ToDiaryNoteDto());
        }

        [HttpPost]
        public IActionResult Create([FromBody] CreateDiaryNoteRequestDto diaryNoteDto)
        {
            var diaryNoteModel = diaryNoteDto.ToDiaryNoteFromCreateDto();
            _context.DiaryNotes.Add(diaryNoteModel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = diaryNoteModel.Id }, diaryNoteModel.ToDiaryNoteDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateDiaryNoteRequestDto updateDto)
        {
            var diaryNoteModel = _context.DiaryNotes.FirstOrDefault(x => x.Id == id);

            if(diaryNoteModel == null)
            {
                return NotFound();
            }

            diaryNoteModel.Title = updateDto.Title;
            diaryNoteModel.Text = updateDto.Text;
            diaryNoteModel.Rating = updateDto.Rating;
            diaryNoteModel.IsPublic = updateDto.IsPublic;

            _context.SaveChanges();

            return Ok(diaryNoteModel.ToDiaryNoteDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var diaryNoteModel = _context.DiaryNotes.FirstOrDefault(x => x.Id ==id);
            
            if(diaryNoteModel == null)
            {
                return NotFound();
            }

            _context.DiaryNotes.Remove(diaryNoteModel);
            _context.SaveChanges();
            
            return NoContent();
        }

    
    }
}
