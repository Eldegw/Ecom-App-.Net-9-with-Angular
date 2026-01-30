using AutoMapper;
using Ecom.Core.Dto;
using Ecom.Core.Entities.Order;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositries.service
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, AppDbContext context, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
        }


        public async Task<Orders> CreateOrderAsync(OrderDto orderDto, string BuyerEmail)
        {
            var basket = await _unitOfWork.castumerBasket.GetBasketAsync(orderDto.basketId);
            List<OrderItem> orderItems = new List<OrderItem>();

            foreach (var item in basket.BasketItems)
            {
                var Product = await _unitOfWork.ProductRepository.GetByIdAsync(item.Id);
                var orderItem = new OrderItem(Product.Id, Product.Name, item.Image, item.Price, item.Quantity);
                orderItems.Add(orderItem);
            }

            var deliveryMethod = await _context.DeliveryMethods.FirstOrDefaultAsync(x=>x.Id == orderDto.deliveryMethodId);

            var subTotal = orderItems.Sum(m=>m.Price * m.Quantity);

            var ship = _mapper.Map<ShippingAddress>(orderDto.shippingAddress);

            var order = new Orders(BuyerEmail, subTotal, ship, deliveryMethod, orderItems);

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            await _unitOfWork.castumerBasket.DeleteBasketAsync(orderDto.basketId);
            return order;


        }


        public async Task<IReadOnlyList<OrderToReturnDto>> GetAllOrdersForUserAsync(string BuyerEmail)
        {
          var orders = await _context.Orders.Where(m=>m.BuyerEmail == BuyerEmail)
                .Include(inc=>inc.orderItems).Include(inc=>inc.deliveryMethod)
                .ToListAsync();
        
            var result = _mapper.Map<IReadOnlyList<OrderToReturnDto>>(orders);
           
            return result;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetAllDeliveryMethod()
        {
          return await _context.DeliveryMethods.AsNoTracking().ToListAsync();
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(int Id, string BuyerEmail)
        {
            var order = await _context.Orders
            .Where(x => x.Id == Id && x.BuyerEmail == BuyerEmail)
            .Include(inc=>inc.orderItems).Include(inc=>inc.deliveryMethod)
            .FirstOrDefaultAsync();
          
            var result = _mapper.Map<OrderToReturnDto>(order);
            return result;
          
        }

       
    }
}
