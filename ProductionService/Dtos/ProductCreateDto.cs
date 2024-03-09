using System.ComponentModel.DataAnnotations;

namespace ProductionService.Dtos
{
    public class ProductCreateDto
    {
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string Cost { get; set; }
    }
}