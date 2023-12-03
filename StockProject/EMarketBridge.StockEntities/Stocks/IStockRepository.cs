using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.StockEntities.Stocks
{
    public interface IStockRepository:IRepository<Stock,Guid>
    {
        Task<Stock?> GetStockByProductId(string productId);
    }
}
