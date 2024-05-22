using WellBeingDiary.Entities;

namespace WellBeingDiary.Interfaces
{
    public interface IUserRepository
    {
        Task<List<AppUser>> GetAllAsync();
        Task<AppUser?> GetByIdAsync(string id);
        Task<IEnumerable<string>> GetUserRolesAsync(string username);
        Task<AppUser?> DeleteAsync(string id);
        string? GetMyId();
        Task<AppUser?> GetMeAsync();
    }
}
