using AutoMapper;
using EMarketBridge.OrderApplication.Features.Orders.Commands.Requests;
using EMarketBridge.OrderApplication.Features.Orders.DTOs;
using EMarketBridge.OrderCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.OrderApplication
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateOrderCommandRequest, Order>().ReverseMap();
            CreateMap<OrderItemDTO, OrderItem>().ReverseMap();
        }
    }
}
