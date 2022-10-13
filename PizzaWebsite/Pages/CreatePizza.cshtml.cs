using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Build.Framework;
using PizzaWebsite.Services;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace PizzaWebsite.Pages
{
    [Authorize("Admin")]
    public class CreatePizzaModel : PageModel
    {
        [BindProperty]
        public CreatePizzaCommand Input { get; set; }

        private PostService _postService;

        public CreatePizzaModel(PostService postService)
        {
            _postService = postService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _postService.AddPizzaAsync(Input);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return Page();
                }

                return RedirectToPage("Index");
            }

            return Page();
        }
    }

    public class CreatePizzaCommand
    {
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string? Name { get; set; }

        [Required]
        [Range(0.1, 1000)]
        public float Price { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(300)]
        public string? Ingredients { get; set; }

        [Required]
        public IFormFile Image { get; set; }

        [Required]
        public IFormFile BackgroundImage { get; set; }

        [Required]
        public bool IsSpecial { get; set; }
    }
}
