using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WellBeingDiary.Dtos.DiaryNote;
using WellBeingDiary.Interfaces;
using WellBeingDiary.Mappers;

namespace WellBeingDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiaryNotesController : ControllerBase
    {
        private readonly IDiaryNoteRepository _diaryRepo;

        public DiaryNotesController(IDiaryNoteRepository diaryRepo)
        {
            _diaryRepo = diaryRepo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var diaryNotes = await _diaryRepo.GetAllAsync();

            var diaryNoteDto = diaryNotes.Select(d => d.ToDiaryNoteDto());

            return Ok(diaryNoteDto);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var diaryNote = await _diaryRepo.GetByIdAsync(id);

            if(diaryNote == null)
            {
                return NotFound();
            }

            return Ok(diaryNote.ToDiaryNoteDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDiaryNoteRequestDto diaryNoteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var diaryNoteModel = diaryNoteDto.ToDiaryNoteFromCreateDto();

            await _diaryRepo.CreateAsync(diaryNoteModel);

            return CreatedAtAction(nameof(GetById), new { id = diaryNoteModel.Id }, diaryNoteModel.ToDiaryNoteDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDiaryNoteRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var diaryNoteModel = await _diaryRepo.UpdateAsync(id, updateDto);

            if(diaryNoteModel == null)
            {
                return NotFound();
            }

            return Ok(diaryNoteModel.ToDiaryNoteDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var diaryNoteModel = await _diaryRepo.DeleteAsync(id);
            
            if(diaryNoteModel == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}
