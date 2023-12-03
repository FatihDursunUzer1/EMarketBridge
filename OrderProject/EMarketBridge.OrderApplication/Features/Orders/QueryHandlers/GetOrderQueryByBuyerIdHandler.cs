using AutoMapper;
using EMarketBridge.OrderApplication.Features.Orders.DTOs;
using EMarketBridge.OrderApplication.Features.Orders.Queries.Requests;
using EMarketBridge.OrderApplication.Features.Orders.Queries.Responses;
using EMarketBridge.OrderCore.Entities;
using EMarketBridge.OrderInfrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.OrderApplication.Features.Orders.QueryHandlers
{
    internal class GetOrderQueryByBuyerIdHandler : IRequestHandler<GetOrdersByBuyerIdQueryRequest, GetOrdersByBuyerIdQueryResponse>
    {
        private readonly OrderDbContext _orderDbContext;
        private readonly IMapper _mapper;

        public GetOrderQueryByBuyerIdHandler(OrderDbContext orderDbContext, IMapper mapper)
        {
            _orderDbContext = orderDbContext;
            _mapper = mapper;
        }

        public async Task<GetOrdersByBuyerIdQueryResponse> Handle(GetOrdersByBuyerIdQueryRequest request, CancellationToken cancellationToken)
        {
            List<Order> orders = await _orderDbContext.Orders.Where(order => order.BuyerId == request.BuyerId).Include(order => order.OrderItems).ToListAsync();
            List<List<OrderItemDTO>> orderItemDtos = new();
            foreach(var order in orders)
            {
                orderItemDtos.Add(_mapper.Map<List<OrderItemDTO>>(order.OrderItems));
            }
            GetOrdersByBuyerIdQueryResponse getOrdersByBuyerIdQueryResponse = new(orderItemDtos);
            return getOrdersByBuyerIdQueryResponse;
        }
    }
}
