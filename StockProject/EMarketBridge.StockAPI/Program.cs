using EMarketBridge.Shared;
using EMarketBridge.StockBusiness;
using EMarketBridge.StockBusiness.Consumers;
using EMarketBridge.StockBusiness.Services;
using EMarketBridge.StockDataAccess.Repositories;
using EMarketBridge.StockEntities;
using EMarketBridge.StockEntities.Stocks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(configurator =>
{
    configurator.AddConsumer<OrderCreatedEventConsumer>();
    configurator.UsingRabbitMq((context, _configure) =>
    {
        _configure.Host(builder.Configuration["RabbitMQ"]);
        _configure.ReceiveEndpoint(RabbitMQSettings.Stock_Order_Created_Queue, e => e.ConfigureConsumer<OrderCreatedEventConsumer>(context));
    });
});

builder.Services.AddTransient<IStockService, StockService>();
builder.Services.AddSingleton(typeof(IRepository<,>),typeof(Repository<,>));
builder.Services.AddSingleton<IStockRepository, StockRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();




app.MapPost("/add-stock",async (IStockService stockService,[FromBody] StockDTO stock) =>
{
    return await stockService.AddStock(stock);
});

app.MapGet("/get-stocks", (IStockService stockService) =>
{
    return stockService.GetAllStock();
});

app.Run();
