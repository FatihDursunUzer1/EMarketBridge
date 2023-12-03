using EMarketBridge.Shared.Events;
using EMarketBridge.StockBusiness.Services;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.StockBusiness.Consumers
{
    public class OrderCreatedEventConsumer : IConsumer<OrderCreatedEvent>
    {
        private readonly IStockService _stockService;
        private readonly IPublishEndpoint _publish;

        public OrderCreatedEventConsumer(IStockService stockService, IPublishEndpoint publish)
        {
            _stockService = stockService;
            _publish = publish;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var isStockEnough = true;
            foreach(var orderItem in context.Message.OrderItems)
            {
                var stock =await _stockService.GetStockByProductId(orderItem.ProductId.ToString());
                if(stock==null || stock.Count < orderItem.Count)
                {
                    isStockEnough = false;
                }
            }

            if(isStockEnough)
            {
                foreach(var orderItem in context.Message.OrderItems)
                {
                    var stock = await _stockService.GetStockByProductId(orderItem.ProductId.ToString());
                    stock!.Count -= orderItem.Count;
                    await _stockService.UpdateStock(stock);
                }
                //StockReservedForPaymentAPI
            }
            else if(!isStockEnough)
                    {
                //StockFailedForOrderAPI
                var stockFailed = new StockOrderFailedEvent
                {
                    OrderId = context.Message.OrderId,
                    BuyerId = context.Message.BuyerId,
                    OrderItems = context.Message.OrderItems
                };
                await _publish.Publish(stockFailed);
                    }
        }
    }
}
