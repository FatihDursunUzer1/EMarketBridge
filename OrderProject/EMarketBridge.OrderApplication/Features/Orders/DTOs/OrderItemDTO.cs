using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.OrderApplication.Features.Orders.DTOs
{
    public record OrderItemDTO(Guid ProductId,int Count)
    {
    }
}
