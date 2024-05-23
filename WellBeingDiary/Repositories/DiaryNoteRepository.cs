using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WellBeingDiary.Data;
using WellBeingDiary.Dtos.DiaryNote;
using WellBeingDiary.Entities;
using WellBeingDiary.Interfaces;

namespace WellBeingDiary.Repositories
{
    public class DiaryNoteRepository : IDiaryNoteRepository
    {
        private readonly DiaryDbContext _context;
        private readonly IUserRepository _userRepo;
        public DiaryNoteRepository(DiaryDbContext context, IUserRepository userRepo)
        {
            _context = context;
            _userRepo = userRepo;
        }

        public async Task<List<DiaryNote>> GetAllAsync()
        {
            return await _context.DiaryNotes.ToListAsync();
        }

        public async Task<List<DiaryNote>> GetAllMyAsync(string userId)
        {
            return await _context.DiaryNotes.Where(note => note.UserId == userId).ToListAsync();
        }

        public async Task<DiaryNote?> GetMyByIdAsync(int diaryNoteId, string userId)
        {
            return await _context.DiaryNotes.FirstOrDefaultAsync(x => x.Id == diaryNoteId && x.UserId == userId);
        }

        public async Task<DiaryNote?> GetByIdAsync(int id)
        {
            return await _context.DiaryNotes.FindAsync(id);
        }

        public async Task<DiaryNote> CreateAsync(DiaryNote diaryModel)
        {
            await _context.DiaryNotes.AddAsync(diaryModel);
            await _context.SaveChangesAsync();

            return diaryModel;
        }

        public async Task<DiaryNote?> UpdateAsync(int id, UpdateDiaryNoteRequestDto diaryDto)
        {
            var diaryNoteModel = await _context.DiaryNotes.FirstOrDefaultAsync(x => x.Id == id);

            if (diaryNoteModel is null)
                return null;

            diaryNoteModel.Title = diaryDto.Title;
            diaryNoteModel.Text = diaryDto.Text;
            diaryNoteModel.Rating = diaryDto.Rating;
            diaryNoteModel.IsPublic = diaryDto.IsPublic;

            await _context.SaveChangesAsync();

            return diaryNoteModel;
        }

        public async Task<DiaryNote?> DeleteAsync(int id)
        {
            var diaryNoteModel = await _context.DiaryNotes.FirstOrDefaultAsync(x => x.Id == id);

            if (diaryNoteModel is null)
                return null;

            _context.DiaryNotes.Remove(diaryNoteModel);
            await _context.SaveChangesAsync();

            return diaryNoteModel;
        }
    }
}
