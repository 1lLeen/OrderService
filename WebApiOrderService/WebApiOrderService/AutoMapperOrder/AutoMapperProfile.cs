using WebApiOrderService.Models.DtoOrders;
using WebApiOrderService.Models.OrderModels;
using AutoMapper;
namespace WebApiOrderService.AutoMapperOrder
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Order, DtoOrder>()
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.OrderName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.OrderDescription, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.OrderPrice, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.FormatedPrice, opt => opt.Ignore());
            CreateMap<Order, DtoOrder>()
               .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.OrderName, opt => opt.MapFrom(src => src.Name))
               .ForMember(dest => dest.OrderDescription, opt => opt.MapFrom(src => src.Description))
               .ForMember(dest => dest.OrderPrice, opt => opt.MapFrom(src => src.Price))
               .ForMember(dest => dest.FormatedPrice, opt => opt.Ignore()).ReverseMap();
        }
    }
}

