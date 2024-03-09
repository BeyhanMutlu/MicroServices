using System.Text.Json;
using AutoMapper;
using OrderService.Dtos;
using OrderService.Interfaces;
using OrderService.Models;

namespace OrderService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;
        public EventProcessor(IServiceScopeFactory scopeFactory,
            IMapper mapper)
        {
            _scopeFactory=scopeFactory;
            _mapper=mapper;
        }
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);

            switch(eventType)
            {
                case EventType.ProductPublished:
                   addProduct(message);
                   break;
                default:
                    break;
            }
        }
        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("---> Determining Event.");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            switch(eventType.Event)
            {
                case "Product_Published":
                    Console.WriteLine("---> Product Published event detected.");
                    return EventType.ProductPublished;
                default:
                    Console.WriteLine("---> Could not determine event detected.");
                    return EventType.Undetermined;
            }
        }
        private void addProduct(string productPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<IOrderRepo>();

                var productPublishedDto = JsonSerializer.Deserialize<ProductionPublishedDto>(productPublishedMessage);

                try
                {
                    var prd = _mapper.Map<Product>(productPublishedDto);
                    if(!repo.ExternalProductExist(prd.ExternalID))
                    {
                        repo.CreateProduct(prd);
                        repo.SaveChanges();
                        Console.WriteLine("---> Product added.");
                    }
                    else
                    {
                        Console.WriteLine("---> Product already exists.");
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Could not add Product to DB {ex.Message}");
                }
            }
        }
    }

    enum EventType
    {
        ProductPublished,
        Undetermined
    }
}