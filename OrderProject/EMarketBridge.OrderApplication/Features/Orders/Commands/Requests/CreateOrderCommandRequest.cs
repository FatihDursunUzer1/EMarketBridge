using EMarketBridge.OrderApplication.Features.Orders.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.OrderApplication.Features.Orders.Commands.Requests
{
    public record CreateOrderCommandRequest(Guid BuyerId, List<OrderItemDTO> OrderItems) : IRequest<bool>
    {
    }
}
