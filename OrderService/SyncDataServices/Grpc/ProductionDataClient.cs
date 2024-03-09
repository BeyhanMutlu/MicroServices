using AutoMapper;
using OrderService.Interfaces;
using OrderService.Models;
using Grpc.Net.Client;
using ProductionService;

namespace OrderService.SyncDataServices.Grpc
{
    public class ProductionDataClient : IProductionDataClient
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public ProductionDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }
        public IEnumerable<Product> ReturnAllProducts()
        {
            Console.WriteLine($"---> Calling GRPC Service. {_configuration["GrpcProduction"]}");
            var channel = GrpcChannel.ForAddress(_configuration["GrpcProduction"]);
            var client = new GrpcProduction.GrpcProductionClient(channel);          
            var request = new GetAllRequest();
            try
            {
                var reply = client.GetAllProducts(request);
                return _mapper.Map<IEnumerable<Product>>(reply.Product);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"--> Could not call GRPC server {ex.Message}");
                return null;
            }

        }
    }
}