using EMarketBridge.OrderApplication.CrossCuttingConcerns.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.OrderApplication
{
    public static class ApplicationRegistration
    {
        public static void ApplicationRegister(this IServiceCollection services)
            {
            services.AddAutoMapper(cfg=>cfg.AddProfile<MappingProfile>());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblyContaining(typeof(OrderItemValidator));
        }
    }
}
