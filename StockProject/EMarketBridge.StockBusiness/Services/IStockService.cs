using EMarketBridge.StockEntities.Stocks;

namespace EMarketBridge.StockBusiness.Services
{
    public interface IStockService
    {
        List<Stock> GetAllStock();
        Task<Stock> AddStock(StockDTO newStock);

        Task<Stock?> GetStockByProductId(string productId);
        Task<Stock> UpdateStock(Stock stock);
    }
}