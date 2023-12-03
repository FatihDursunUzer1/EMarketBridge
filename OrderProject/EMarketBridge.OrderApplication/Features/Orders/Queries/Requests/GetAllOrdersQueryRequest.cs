using EMarketBridge.OrderApplication.Features.Orders.Queries.Responses;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.OrderApplication.Features.Orders.Queries.Requests
{
    public class GetAllOrdersQueryRequest:IRequest<GetAllOrdersQueryResponse>
    {
    }
}
