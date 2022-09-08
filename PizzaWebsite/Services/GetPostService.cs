using Microsoft.EntityFrameworkCore;
using PizzaWebsite.Data;

namespace PizzaWebsite.Services
{
    public class GetPostService
    {
        private ApplicationDbContext _context;

        public GetPostService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Pizza>> GetPizzasAsync()
        {
            return await _context.Pizzas
                .Select(x => new Pizza
                {
                    Name = x.Name,
                    PizzaId = x.PizzaId,
                    Ingredients = x.Ingredients,
                    Price = x.Price
                })
                .ToListAsync();
        }
    }
}
