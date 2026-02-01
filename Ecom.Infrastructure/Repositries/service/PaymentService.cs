using Ecom.Core.Entities;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Infrastructure.Repositries.service
{
    public class PaymentService : IPaymentService
    {
       
        private readonly IUnitOfWork work;
        private readonly IConfiguration configuration;
        private readonly AppDbContext _context;

        public PaymentService(IUnitOfWork work, IConfiguration configuration, AppDbContext context)
        {
            this.work = work;
            this.configuration = configuration;
            _context = context;
        }



        public async Task<CustomerBasket> CreateOrUpdatePaymentAsync(string basketid, int? deliverMethodId)
        {
            CustomerBasket basket = await work.castumerBasket.GetBasketAsync(basketid);

            StripeConfiguration.ApiKey = configuration["StripSetting:secretKey"];

            decimal shippingPrice = 0m;

            if (deliverMethodId.HasValue)
            {
                var delivery = await _context.DeliveryMethods.AsNoTracking()
                     .FirstOrDefaultAsync(x => x.Id == deliverMethodId.Value);
                shippingPrice = delivery.Price;

            }
            foreach (var item in basket.BasketItems)
            {
                var product = await work.ProductRepository.GetByIdAsync(item.Id);

                item.Price = product.NewPrice;
            } 

            PaymentIntentService paymentIntentService = new PaymentIntentService();

            PaymentIntent _intent;

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                var option = new PaymentIntentCreateOptions()
                {
                    Amount = (long)basket.BasketItems.Sum(m => m.Quantity * (m.Price * 100)) + (long)(shippingPrice * 100),
                    Currency = "USD",
                    PaymentMethodTypes = new List<string> { "card"}

                };

                _intent = await paymentIntentService.CreateAsync(option);
                basket.PaymentIntentId = _intent.Id;
                basket.ClientSecret = _intent.ClientSecret;
            }
            else
            {
                var option = new PaymentIntentUpdateOptions()
                {
                    Amount = (long)basket.BasketItems.Sum(m => m.Quantity * (m.Price * 100)) + (long)(shippingPrice * 100)

                };
                await paymentIntentService.UpdateAsync(basket.PaymentIntentId, option);
            }

            await work.castumerBasket.UpdateBasketAsync(basket);
            return basket;
        }

       
    }
}