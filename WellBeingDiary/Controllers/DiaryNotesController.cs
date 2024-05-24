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
        private readonly IUserRepository _userRepo;

        public DiaryNotesController(IDiaryNoteRepository diaryRepo, IUserRepository userRepo)
        {
            _diaryRepo = diaryRepo;
            _userRepo = userRepo;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var diaryNotes = await _diaryRepo.GetAllAsync();

            var diaryNoteDto = diaryNotes.Select(d => d.ToDiaryNoteDto());

            return Ok(diaryNoteDto);
        }

        [HttpGet("my")]
        [Authorize]
        public async Task<IActionResult> GetAllMy()
        {
            var myId = _userRepo.GetMyId();

            if (string.IsNullOrEmpty(myId))
                return Unauthorized();

            var diaryNotes = await _diaryRepo.GetAllMyAsync(myId);

            var diaryNotesDto = diaryNotes.Select(d => d.ToDiaryNoteDto());

            return Ok(diaryNotesDto);
        }

        [HttpGet("me/{id:int}")]
        [Authorize]
        public async Task<IActionResult> GetMyById([FromRoute] int diaryNoteId)
        {
            var myId = _userRepo.GetMyId();

            if (string.IsNullOrEmpty(myId))
                return Unauthorized();

            var diaryNote = await _diaryRepo.GetMyByIdAsync(diaryNoteId, myId);

            if (diaryNote is null)
                return NotFound();

            return Ok(diaryNote);
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var diaryNote = await _diaryRepo.GetByIdAsync(id);

            if(diaryNote is null)
                return NotFound();

            return Ok(diaryNote.ToDiaryNoteDto());
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateDiaryNoteRequestDto diaryNoteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var myId = _userRepo.GetMyId();

            if (string.IsNullOrEmpty(myId))
                return Unauthorized();

            var diaryNoteModel = diaryNoteDto.ToDiaryNoteFromCreateDto(myId);

            await _diaryRepo.CreateAsync(diaryNoteModel);

            return CreatedAtAction(nameof(GetById), new { id = diaryNoteModel.Id }, diaryNoteModel.ToDiaryNoteDto());
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDiaryNoteRequestDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var diaryNoteModel = await _diaryRepo.UpdateAsync(id, updateDto);

            if(diaryNoteModel is null)
                return NotFound();

            return Ok(diaryNoteModel.ToDiaryNoteDto());
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var diaryNoteModel = await _diaryRepo.DeleteAsync(id);
            
            if(diaryNoteModel is null)
                return NotFound();
            
            return NoContent();
        }

        [HttpDelete("me/{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteMy([FromRoute] int id)
        {
            var myId = _userRepo.GetMyId();

            if (string.IsNullOrEmpty(myId))
                return Unauthorized();

            var myDiaryNoteModel = await _diaryRepo.GetMyByIdAsync(id, myId);

            if (myDiaryNoteModel is null)
                return NotFound();

            await _diaryRepo.DeleteAsync(id);

            return NoContent();
        }
    }
}
