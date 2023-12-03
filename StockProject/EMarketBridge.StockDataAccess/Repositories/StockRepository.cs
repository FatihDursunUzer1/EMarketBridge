using EMarketBridge.StockEntities;
using EMarketBridge.StockEntities.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.StockDataAccess.Repositories
{
    public class StockRepository : Repository<Stock, ObjectId>, IStockRepository
    {
        public async Task<Stock?> GetStockByProductId(string productId)
        {
            return await stockDbContext.Set<Stock>().FirstOrDefaultAsync(stock => stock.ProductId == productId);
        }
    }
}
