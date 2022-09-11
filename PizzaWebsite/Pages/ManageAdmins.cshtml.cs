using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PizzaWebsite.Data;
using PizzaWebsite.Services;
using System.Security.Claims;

namespace PizzaWebsite.Pages
{
    [Authorize("Admin")]
    public class ManageAdminsModel : PageModel
    {
        public List<ApplicationUser> Admins { get; set; } = new();
        public List<ApplicationUser> NotAdmins { get; set; } = new();

        private readonly UserManager<ApplicationUser> _userManager;

        public ManageAdminsModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGet()
        {
            var admins = await _userManager.GetUsersForClaimAsync(new Claim("Admin", "true"));
            Admins = admins.ToList();

            NotAdmins = await _userManager.Users.Where(x => !admins.Contains(x)).ToListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostMakeAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.AddClaimAsync(user, new Claim("Admin", "true"));
            
            return RedirectToPage("ManageAdmins");
        }

        public async Task<IActionResult> OnPostRemoveAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.RemoveClaimAsync(user, new Claim("Admin", "true"));

            return RedirectToPage("ManageAdmins");
        }
    }
}
