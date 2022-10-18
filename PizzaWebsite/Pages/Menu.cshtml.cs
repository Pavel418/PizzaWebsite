using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PizzaWebsite.Data;
using PizzaWebsite.Services;

namespace PizzaWebsite.Pages
{
    public class MenuModel : PageModel
    {
        [BindProperty]
        public string SortingChoice { get; set; }
        public IEnumerable<SelectListItem> Items { get; set; } = new List<SelectListItem>
        {
            new SelectListItem {Value = "Price", Text="Cost"},
            new SelectListItem {Value = "Alphabet", Text="A-Z"},
            new SelectListItem { Value = "Random", Text="Random", Selected = true}
        };

        public ICollection<Pizza> Pizzas { get; set; }

        private readonly PostService _service;
        private List<int> testsId;

        public MenuModel(PostService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnGetAsync(string sortingChoice, int pageIndex, int pizzasPerPage)
        {
            try
            {
                Pizzas = await _service.GetChunkOfPizzas(pizzasPerPage, pageIndex, Enum.Parse<SortMethod>(sortingChoice));
            }
            catch
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost()
        {
            return RedirectToPage(routeValues: new { SortingChoice });
        }

        public async Task<IActionResult> OnPostFill()
        {
            testsId = await _service.AddTestPizzas(300);

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteTests()
        {
            if (testsId == null)
                return Page();

            foreach (int id in testsId)
            {
                await _service.DeletePizza(id);
            }

            /*
            var pizzas = await _service.GetPizzas();

            foreach (var pizza in pizzas)
            {
                if (pizza.ImageLocation == "")
                    await _service.DeletePizza(pizza.PizzaId);
            }
            */

            return RedirectToPage();
        }
    }

    public enum SortMethod{ Random, Price, Alphabet}
}
