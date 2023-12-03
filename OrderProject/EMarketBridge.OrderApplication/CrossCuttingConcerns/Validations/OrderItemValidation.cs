using EMarketBridge.OrderApplication.Features.Orders.DTOs;
using EMarketBridge.OrderCore.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.OrderApplication.CrossCuttingConcerns.Validations
{
    public class OrderItemValidator : AbstractValidator<OrderItemDTO>
    {
        public OrderItemValidator()
        {
            RuleFor(x => x.ProductId).NotNull();
            RuleFor(x => x.Count).GreaterThan(0);
        }
    }
}
