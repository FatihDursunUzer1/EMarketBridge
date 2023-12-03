using EMarketBridge.OrderApplication.Features.Orders.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.OrderApplication.Features.Orders.Queries.Responses
{
    public record GetAllOrdersQueryResponse(List<List<OrderItemDTO>> OrderItemDTOs)
    {
    }
}
