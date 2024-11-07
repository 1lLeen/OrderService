using AutoMapper;
using DeliveryService.Models.DeliveriesModels;
using DeliveryService.Models.DtoDeliveries;

namespace DeliveryService.AutoMapperDelivery
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Delivery, DtoDelivery>()
                .ForMember(dest => dest.DeliveryId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DeliveryOrderName, opt => opt.MapFrom(src => src.OrderName))
                .ForMember(dest => dest.DeliveryPriceDelivery, opt => opt.MapFrom(src => src.PriceDelivery))
                .ForMember(dest => dest.DeliveryFrom, opt => opt.MapFrom(src => src.From))
                .ForMember(dest => dest.DeliveryTo, opt => opt.MapFrom(src => src.To))
                .ForMember(dest => dest.DeliveryDeadyline, opt => opt.MapFrom(src => src.DeadLine));
            CreateMap<Delivery, DtoDelivery>()
               .ForMember(dest => dest.DeliveryId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.DeliveryOrderName, opt => opt.MapFrom(src => src.OrderName))
               .ForMember(dest => dest.DeliveryPriceDelivery, opt => opt.MapFrom(src => src.PriceDelivery))
               .ForMember(dest => dest.DeliveryFrom, opt => opt.MapFrom(src => src.From))
               .ForMember(dest => dest.DeliveryTo, opt => opt.MapFrom(src => src.To))
               .ForMember(dest => dest.DeliveryDeadyline, opt => opt.MapFrom(src => src.DeadLine)).ReverseMap();
        }

    }
}
