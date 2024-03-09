using ProductionService.Dtos;

namespace ProductionService.Interfaces
{
    public interface IOrderDataClient
    {
        Task SendProductToOrder(ProductReadDto prd);
    }
}