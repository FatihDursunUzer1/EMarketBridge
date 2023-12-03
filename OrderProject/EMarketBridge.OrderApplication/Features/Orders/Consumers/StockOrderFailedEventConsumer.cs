using EMarketBridge.OrderCore.Entities;
using EMarketBridge.OrderInfrastructure.Context;
using EMarketBridge.Shared.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.OrderApplication.Features.Orders.Consumers
{
   public class StockOrderFailedEventConsumer : IConsumer<StockOrderFailedEvent>
    {
        private readonly OrderDbContext _orderDbContext;

        public StockOrderFailedEventConsumer(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task Consume(ConsumeContext<StockOrderFailedEvent> context)
        {
            var order = _orderDbContext.Set<Order>().Where(order=>order.Id==context.Message.OrderId).FirstOrDefault();
            order!.OrderStatus = OrderStatus.Fail;

            _orderDbContext.Update(order);
            await _orderDbContext.SaveChangesAsync();


        }
    }
}
