using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaWebsite.Data;

namespace PizzaWebsite.Pages
{
    public class IndexModel : PageModel
    {
        public List<Pizza> SpecialPizzas { get; set; } = new();

        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            SpecialPizzas = _context.Pizzas.Where(x => x.IsSpecialOffer).ToList();
        }
    }
}