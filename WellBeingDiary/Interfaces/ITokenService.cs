using WellBeingDiary.Entities;

namespace WellBeingDiary.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
        void SetTokenCookie(HttpContext httpContext, string token);
    }
}
