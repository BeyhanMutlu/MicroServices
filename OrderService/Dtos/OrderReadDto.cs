namespace OrderService.Dtos
{
    public class OrderReadDto
    {
        public int Id { get; set; }
        public string HowMany { get; set; }
        public int ProductId { get; set; }
    }
}