using System.ComponentModel.DataAnnotations;

namespace OrderService.Models
{
    public class Product
    {
        [Key,Required]
        public int Id { get; set; }
        [Required]
        public int ExternalID { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();

    }
}