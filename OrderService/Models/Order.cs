using System.ComponentModel.DataAnnotations;

namespace OrderService.Models
{
    public class Order
    {
        [Key,Required]
        public int Id { get; set; }
        [Required]
        public int HowMany { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}