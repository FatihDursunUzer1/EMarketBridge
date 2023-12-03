using EMarketBridge.StockEntities.Stocks;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore.Extensions;
using System.Reflection.Emit;

namespace EMarketBridge.StockDataAccess.Context
{
    public class StockDbContext:DbContext
    {
        public StockDbContext(DbContextOptions options):base(options)
        {

        }
        public static StockDbContext Create(IMongoDatabase database) =>
           new(new DbContextOptionsBuilder<StockDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Stock>().ToCollection("movies");
        }

        public DbSet<Stock> Stocks { get; set; }

    }
}
