using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaWebsite.Data;
using PizzaWebsite.Services;

namespace PizzaWebsite.Pages
{
    public class ViewPizzaModel : PageModel
    {
        public bool IsAdmin { get; set; }
        [BindProperty]
        public bool ChangeSpecial { get; set; }

        private readonly PostService _postService; 
        private readonly IAuthorizationService _authService;

        public ViewPizzaModel(PostService postService, IAuthorizationService authorization)
        {
            _postService = postService;
            _authService = authorization;
        }

        [BindProperty]
        public Pizza Pizza { get; set; } 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var isAuthorised = await _authService.AuthorizeAsync(User, policyName: "Admin");
            IsAdmin = isAuthorised.Succeeded;

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

        public async Task<IActionResult> OnPostDeleteAsync(int? id)
        {
            var authResult = await _authService.AuthorizeAsync(User, policyName: "Admin");
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

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

        public async Task<IActionResult> OnPostChangeSpecialAsync(int id)
        {
            var authResult = await _authService.AuthorizeAsync(User, policyName: "Admin");
            if (!authResult.Succeeded)
            {
                return new ForbidResult();
            }

            try
            {
                await _postService.ChangeSpecialOffer(!ChangeSpecial, id);
            }
            catch (Exception)
            {
                return NotFound();
            }
            return RedirectToPage("ViewPizza", new { id });
        }
    }
}
