using Ecom.Core.Dto;
using Ecom.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> Create(OrderDto orderDto)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderService.CreateOrderAsync(orderDto,email);
            return Ok(order);
        }

        [HttpGet("get-orders-for-user")]
        public async Task<IActionResult> GetAllOrders()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
           var order = await _orderService.GetAllOrdersForUserAsync(email);
           
            return Ok(order);


        }
        [HttpGet("get-order-by-id/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            var order = await _orderService.GetOrderByIdAsync(id, email);

            return Ok(order);

        }

        [HttpGet("get-delivery")]
        public async Task<IActionResult> GetAllDelivery()
        {
            var delivery = await _orderService.GetAllDeliveryMethod();
            return Ok(delivery);
        }


    }
}
