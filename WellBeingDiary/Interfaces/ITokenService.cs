using WellBeingDiary.Entities;

namespace WellBeingDiary.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
