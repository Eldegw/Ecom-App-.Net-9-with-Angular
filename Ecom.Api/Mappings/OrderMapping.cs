using AutoMapper;
using Ecom.Core.Dto;
using Ecom.Core.Entities.Order;

namespace Ecom.Api.Mappings
{
    public class OrderMapping:Profile
    {
        public OrderMapping()
        {

            CreateMap<Orders , OrderToReturnDto>()
                .ForMember(x=>x.deliveryMethod,x=>x
                .MapFrom(x=>x.deliveryMethod.Name))
                .ReverseMap();


            CreateMap<OrderItem , OrderItemDto>().ReverseMap();
            CreateMap<ShippingAddress, ShippingAddressDto>().ReverseMap();


        }
    }
}
