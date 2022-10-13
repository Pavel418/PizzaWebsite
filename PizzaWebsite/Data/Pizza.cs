using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzaWebsite.Data
{
    public class Pizza
    {
        [Key]
        public int PizzaId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Ingredients { get; set; }
        public string ImageLocation { get; set; }

        public bool IsSpecialOffer { get; set; }

        public string BackGroundImageLocation { get; set; }
    }
}
