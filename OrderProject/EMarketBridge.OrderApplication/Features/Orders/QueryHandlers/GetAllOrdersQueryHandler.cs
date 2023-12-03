using AutoMapper;
using EMarketBridge.OrderApplication.Features.Orders.DTOs;
using EMarketBridge.OrderApplication.Features.Orders.Queries.Requests;
using EMarketBridge.OrderApplication.Features.Orders.Queries.Responses;
using EMarketBridge.OrderCore.Entities;
using EMarketBridge.OrderInfrastructure.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace EMarketBridge.OrderApplication.Features.Orders.QueryHandlers
{
   public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQueryRequest, GetAllOrdersQueryResponse>
    {
        private readonly OrderDbContext _orderDbContext;
        private readonly IMapper _mapper;

        public GetAllOrdersQueryHandler(OrderDbContext orderDbContext,IMapper mapper)
        {
            _orderDbContext = orderDbContext;
            _mapper = mapper;
        }

        public async Task<GetAllOrdersQueryResponse> Handle(GetAllOrdersQueryRequest request, CancellationToken cancellationToken)
        {
            List<Order> orders = await _orderDbContext.Orders.Include(order=>order.OrderItems).ToListAsync();
            List<List<OrderItemDTO>> orderItemDtos = new();
            foreach (var order in orders)
            {
                orderItemDtos.Add(_mapper.Map<List<OrderItemDTO>>(order.OrderItems));
            }
            GetAllOrdersQueryResponse getAllOrdersQueryResponse = new(orderItemDtos);
            return getAllOrdersQueryResponse;
        }
    }
}
