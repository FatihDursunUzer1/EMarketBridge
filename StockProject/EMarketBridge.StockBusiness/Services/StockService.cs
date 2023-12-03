using EMarketBridge.StockDataAccess.Repositories;
using EMarketBridge.StockEntities.Stocks;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.StockBusiness.Services
{

    public class StockService:IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public List<Stock> GetAllStock()
        {
            return _stockRepository.GetAll();
        }

        public async Task<Stock> AddStock(StockDTO newStock)
        {
            var stock = new Stock
            {
                ProductId = newStock.ProductId,
                Count = newStock.Count
            };
           return await _stockRepository.Add(stock);
        }

        public async Task<Stock?> GetStockByProductId(string productId)
        {

            return await _stockRepository.GetStockByProductId(productId);
        }

        public Task<Stock> UpdateStock(Stock stock)
        {
            return _stockRepository.Update(stock);
        }
    }
}
