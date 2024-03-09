using System.ComponentModel.DataAnnotations;

namespace ProductionService.Models
{
    public class Product
    {
        [Key,Required]
        public int Id { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Cost { get; set; }

    }

}