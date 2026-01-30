using Ecom.Core.Dto;
using Ecom.Core.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Services
{
    public interface IOrderService
    {
        Task<Orders>CreateOrderAsync(OrderDto orderDto , string BuyerEmail);
        Task<IReadOnlyList<OrderToReturnDto>>GetAllOrdersForUserAsync(string  BuyerEmail);
        Task<OrderToReturnDto> GetOrderByIdAsync(int Id , string BuyerEmail);
        

        Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethod();

    }
}
