

using EMarketBridge.OrderApplication;
using EMarketBridge.OrderApplication.CrossCuttingConcerns.Validations;
using EMarketBridge.OrderApplication.Features.Orders.Commands.Requests;
using EMarketBridge.OrderApplication.Features.Orders.Consumers;
using EMarketBridge.OrderApplication.Features.Orders.DTOs;
using EMarketBridge.OrderApplication.Features.Orders.Queries.Requests;
using EMarketBridge.OrderInfrastructure.Context;
using EMarketBridge.Shared.Events;
using FluentValidation;
using FluentValidation.Results;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderDbContext>();
builder.Services.ApplicationRegister();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<StockOrderFailedEventConsumer>();
    configurator.UsingRabbitMq((context, _configure) =>
    {
        _configure.Host(builder.Configuration["RabbitMQ"]);
        _configure.ReceiveEndpoint("stock-order-failed", e => e.ConfigureConsumer<StockOrderFailedEventConsumer>(context));
    });
});

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();



app.MapPost("/create-order", async (IMediator mediator,IValidator<OrderItemDTO> validator,CreateOrderCommandRequest createOrderCommand) =>
{
    List<List<ValidationFailure>> errors = new();
    foreach (var orderItem in createOrderCommand.OrderItems)
    {
        FluentValidation.Results.ValidationResult result = await validator.ValidateAsync(orderItem);
      
        if(!result.IsValid)
        {
            return Results.ValidationProblem(result.ToDictionary());
        }
    }
    var sendMediator = await mediator.Send(createOrderCommand);
    return Results.Ok(sendMediator);
}).WithDescription("TestDescripton for me because. i will try first time this endpoint style. Good lucks for me");

app.MapGet("/get-order-buyer-id", async (IMediator mediator,[AsParameters] GetOrdersByBuyerIdQueryRequest getOrdersByBuyerIdQueryRequest) =>
{
    var sendMediator = await mediator.Send(getOrdersByBuyerIdQueryRequest);
    return sendMediator;
}).WithName("GetOrdersByBuyerId");

app.Run();
