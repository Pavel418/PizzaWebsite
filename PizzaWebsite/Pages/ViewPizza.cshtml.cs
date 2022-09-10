using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaWebsite.Data;
using PizzaWebsite.Services;

namespace PizzaWebsite.Pages
{
    public class ViewPizzaModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly PostService _postService;

        public ViewPizzaModel(ApplicationDbContext context, IWebHostEnvironment environment, PostService postService)
        {
            _context = context;
            _environment = environment;
            _postService = postService;
        }

        [BindProperty]
        public Pizza Pizza { get; set; } 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            try
            {
                Pizza = await _postService.GetPizzaByID(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            try
            {
                await _postService.DeletePizza(id);
            }
            catch (Exception)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
