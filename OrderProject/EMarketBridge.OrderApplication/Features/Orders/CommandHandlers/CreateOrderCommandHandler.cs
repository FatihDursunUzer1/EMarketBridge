using AutoMapper;
using EMarketBridge.OrderApplication.Features.Orders.Commands.Requests;
using EMarketBridge.OrderCore.Entities;
using EMarketBridge.OrderInfrastructure.Context;
using EMarketBridge.Shared.Events;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.OrderApplication.Features.Orders.CommandHandlers
{
    public class CreateOrderCommandRequestHandler : IRequestHandler<CreateOrderCommandRequest,bool>
    {
        private readonly IPublishEndpoint _publishEndPoint;
        private readonly OrderDbContext _orderDbContext;
        private readonly IMapper _mapper;

        public CreateOrderCommandRequestHandler(OrderDbContext orderDbContext,IMapper mapper,IPublishEndpoint publishEndpoint)
        {
            _orderDbContext = orderDbContext;
            _mapper = mapper;
            _publishEndPoint = publishEndpoint;
        }

        public async Task<bool> Handle(CreateOrderCommandRequest request, CancellationToken cancellationToken)
        {
             Order newOrder = _mapper.Map<Order>(request);
             await _orderDbContext.AddAsync(newOrder,CancellationToken.None);
             await _orderDbContext.SaveChangesAsync(CancellationToken.None);

            var orderCreatedEvent = new OrderCreatedEvent
            {
                BuyerId = newOrder.BuyerId,
                OrderId = newOrder.Id,
                OrderItems = newOrder.OrderItems.Select(order => new OrderItemMessage
                {
                    Id = order.Id,
                    ProductId = order.ProductId,
                    Count = order.Count
                }
                ).ToList()  
            };
           await _publishEndPoint.Publish(orderCreatedEvent);
            return true;
        }
    }
}
