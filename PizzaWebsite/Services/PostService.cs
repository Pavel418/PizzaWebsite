using Microsoft.EntityFrameworkCore;
using PizzaWebsite.Data;
using PizzaWebsite.Pages;
using System.ComponentModel.DataAnnotations;

namespace PizzaWebsite.Services
{
    public class PostService
    {
        private ApplicationDbContext _context;
        private IWebHostEnvironment _environment;

        public PostService(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<ICollection<Pizza>> GetPizzasAsync()
        {
            return await _context.Pizzas
                .Select(x => new Pizza
                {
                    Name = x.Name,
                    PizzaId = x.PizzaId,
                    Ingredients = x.Ingredients,
                    Price = x.Price,
                    ImageLocation = x.ImageLocation
                })
                .ToListAsync();
        }

        public async Task<int> AddPizzaAsync(CreatePizzaCommand command)
        {
            string wwwRootPath = _environment.WebRootPath;
            string fileName = Path.GetRandomFileName();
            string fileExtension = Path.GetExtension(command.Image.FileName);

            string[] imageExtensions = { ".jpeg", ".png", ".jpg", ".bmp", ".jfif" };

            bool isValid = false;
            foreach (string extension in imageExtensions)
            {
                if (extension == fileExtension)
                    isValid = true;
            }

            if (!isValid)
            {
                throw new InvalidOperationException("This file format is not supported.");
            }

            string path = Path.Combine(wwwRootPath, @"img\pizza-images", fileName + fileExtension);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await command.Image.CopyToAsync(fileStream);
            }
            Pizza pizza = new()
            {
                Name = command.Name,
                Price = command.Price,
                Ingredients = command.Ingredients,
                ImageLocation = fileName + fileExtension
            };

            _context.Add(pizza);
            await _context.SaveChangesAsync();

            return pizza.PizzaId;
        }
    }
}
