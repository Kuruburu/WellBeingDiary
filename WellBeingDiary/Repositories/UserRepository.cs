using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WellBeingDiary.Data;
using WellBeingDiary.Entities;
using WellBeingDiary.Interfaces;

namespace WellBeingDiary.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        public UserRepository(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<AppUser>> GetAllAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<AppUser?> GetByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<AppUser?> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user is null)
                return null;

            await _userManager.DeleteAsync(user);
            return user;
        }
    }
}
