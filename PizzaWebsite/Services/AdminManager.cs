using Microsoft.EntityFrameworkCore;
using PizzaWebsite.Data;

namespace PizzaWebsite.Services
{
    public class AdminManager
    {
        private readonly ApplicationDbContext _context;
        public AdminManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ApplicationUser>> GetUsers()
        {
            var users = await _context.Users.Where(x => x.EmailConfirmed == true)
                .ToListAsync();
            return users;
        }
    }
}
