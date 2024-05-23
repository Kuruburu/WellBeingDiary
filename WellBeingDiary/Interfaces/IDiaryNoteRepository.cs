using WellBeingDiary.Dtos.DiaryNote;
using WellBeingDiary.Entities;

namespace WellBeingDiary.Interfaces
{
    public interface IDiaryNoteRepository
    {
        Task<List<DiaryNote>> GetAllAsync();
        Task<List<DiaryNote>> GetAllMyAsync(string id);
        Task<DiaryNote?> GetMyByIdAsync (int diaryNoteId, string userId);
        Task<DiaryNote?> GetByIdAsync(int id);
        Task<DiaryNote> CreateAsync(DiaryNote diaryModel);
        Task<DiaryNote?> UpdateAsync(int id, UpdateDiaryNoteRequestDto diaryDto);
        Task<DiaryNote?> DeleteAsync(int id);
    }
}
