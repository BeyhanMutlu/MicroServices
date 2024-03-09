using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace OrderService.Dtos
{
    public class OrderCreateDto
    {
        [Required]
        public int HowMany { get; set; }          
    }
}