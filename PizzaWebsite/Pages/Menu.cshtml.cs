using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PizzaWebsite.Data;
using PizzaWebsite.Services;

namespace PizzaWebsite.Pages
{
    public class MenuModel : PageModel
    {
        public ICollection<Pizza> Pizzas { get; set; }
        public FileInfo[] ImageSources { get; set; }

        PostService _service;

        public MenuModel(PostService service)
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
