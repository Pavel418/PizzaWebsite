using Microsoft.EntityFrameworkCore;
using PizzaWebsite.Data;
using PizzaWebsite.Pages;
using System.ComponentModel.DataAnnotations;
using System.IO;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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

        public async Task<Pizza> GetPizzaByID(int? id)
        {
            if (id == null || _context.Pizzas == null)
            {
                throw new ArgumentException("ID is null");
            }
            var pizza = await _context.Pizzas.FindAsync(id);
            if (pizza == null)
            {
                throw new ArgumentNullException();
            }

            return pizza;
        }

        public async Task<int> AddPizzaAsync(CreatePizzaCommand command)
        {
            string localFrontImage = await SaveImage(command.Image);

            string localBackground = await SaveImage(command.BackgroundImage);

            Pizza pizza = new()
            {
                Name = command.Name,
                Price = command.Price,
                Ingredients = command.Ingredients,
                ImageLocation = localFrontImage,
                BackGroundImageLocation = localBackground,
                IsSpecialOffer = command.IsSpecial
            };

            _context.Add(pizza);
            await _context.SaveChangesAsync();

            return pizza.PizzaId;
        }

        public async Task DeletePizza(int? id)
        {
            if (id == null || _context.Pizzas == null)
            {
                throw new ArgumentException("ID is null");
            }
            var pizza = await _context.Pizzas.FindAsync(id);

            if (pizza != null)
            {
                _context.Pizzas.Remove(pizza);
                await _context.SaveChangesAsync();

                DeleteImage(pizza.ImageLocation);
                DeleteImage(pizza.BackGroundImageLocation);
            }
        }

        public async Task ChangeSpecialOffer(bool isSpecial, int? id)
        {
            if (id == null || _context.Pizzas == null)
            {
                throw new ArgumentException("ID is null");
            }
            var pizza = await _context.Pizzas.FindAsync(id);

            if (pizza != null)
            {
                pizza.IsSpecialOffer = isSpecial;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<string> SaveImage(IFormFile file)
        {
            string fileName = Path.GetRandomFileName();
            string fileExtension = Path.GetExtension(file.FileName);

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

            string fullName = fileName + fileExtension;

            string path = Path.Combine(_environment.WebRootPath, @"img\pizza-images", fullName);

            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return fullName;
        }

        public void DeleteImage(string fileName)
        {
            string path = Path.Combine(_environment.WebRootPath, @"img\pizza-images", fileName);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
