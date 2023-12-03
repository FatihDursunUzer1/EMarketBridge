using EMarketBridge.OrderApplication.Features.Orders.Queries.Responses;
using EMarketBridge.OrderCore.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.OrderApplication.Features.Orders.Queries.Requests
{
    public record GetOrdersByBuyerIdQueryRequest(Guid BuyerId) : IRequest<GetOrdersByBuyerIdQueryResponse>
    {
        
    }

   /* public class GetOrdersByBuyerIdQueryRequest : IRequest<GetOrdersByBuyerIdQueryResponse>
    {
        public Guid BuyerId { get; set; }
    }*/
}
