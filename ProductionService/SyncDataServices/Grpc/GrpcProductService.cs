using System.Net.Http.Headers;
using AutoMapper;
using Grpc.Core;
using ProductionService.Interfaces;

namespace ProductionService.SyncDataServices.Grpc
{
    public class GrpcProductService: GrpcProduction.GrpcProductionBase
    {
        private readonly IProductRepo _repo;
        private readonly IMapper _mapper;

        public  GrpcProductService(IProductRepo repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;            
        }

        public override Task<ProductResponse> GetAllProducts(GetAllRequest request, ServerCallContext context)
        {
            var response = new ProductResponse();
            var products = _repo.GetAllProducts();
            foreach(var prd in products)
            {
                response.Product.Add(_mapper.Map<GrpcProductModel>(prd));
            }
            return Task.FromResult(response);
        }
    }
}