using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaWebsite.Data;
using PizzaWebsite.Services;

namespace PizzaWebsite.Pages
{
    public class MenuModel : PageModel
    {
        public ICollection<Pizza>? Pizzas { get; set; }

        GetPostService _service;

        public MenuModel(GetPostService service)
        {
            _service = service;
        }


        public async Task<IActionResult> OnGetAsync()
        {
            Pizzas = await _service.GetPizzasAsync();

            return Page();
        }
    }
}
