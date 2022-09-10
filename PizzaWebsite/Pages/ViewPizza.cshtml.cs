﻿using System;
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
        public bool CanDeletePizza { get; set; }

        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly PostService _postService; 
        private readonly IAuthorizationService _authService;

        public ViewPizzaModel(ApplicationDbContext context, IWebHostEnvironment environment, PostService postService, IAuthorizationService authorization)
        {
            _context = context;
            _environment = environment;
            _postService = postService;
            _authService = authorization;
        }

        [BindProperty]
        public Pizza Pizza { get; set; } 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var isAuthorised = await _authService.AuthorizeAsync(User, policyName: "Admin");
            CanDeletePizza = isAuthorised.Succeeded;

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
    }
}
