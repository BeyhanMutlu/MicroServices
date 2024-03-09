using AutoMapper;
using OrderService.Dtos;
using OrderService.Models;
using ProductionService;

namespace OrderService.Profiles
{
    public class OrderProfile: Profile
    {
        public OrderProfile()
        {
            CreateMap<Product,ProductionReadDto>();
            CreateMap<OrderCreateDto,Order>();
            CreateMap<Order,OrderReadDto>();
            CreateMap<ProductionPublishedDto,Product>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.Id));
            CreateMap<GrpcProductModel, Product>()
                .ForMember(dest => dest.ExternalID, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Orders, opt => opt.Ignore());

        }
    }
}