using AutoMapper;
using ProductionService.Dtos;
using ProductionService.Models;

namespace ProductionService.Profiles
{
    public class ProductionProfile : Profile
    {
        public ProductionProfile()
        {
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductCreateDto,Product>();  
            CreateMap<ProductReadDto,ProductPublishedDto>();  
            CreateMap<Product, GrpcProductModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand))
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost));
        }
    }
}