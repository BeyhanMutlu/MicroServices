using ProductionService.Dtos;

namespace ProductionService.Interfaces
{
    public interface IMessageBusClient
    {
        void PublishNewProduct(ProductPublishedDto productPublishedDto);
    }
}