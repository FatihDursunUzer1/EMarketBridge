using EMarketBridge.StockDataAccess.Context;
using EMarketBridge.StockEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.StockDataAccess.Repositories
{
    public class Repository<TEntity,TEntityId>:IRepository<TEntity,TEntityId> where TEntity:class, IEntity<TEntityId>
    {
        private readonly MongoClient client;
        protected readonly StockDbContext stockDbContext;

        public Repository()
        {
            //_configuration = configuration;
            // client = new MongoClient(_configuration.GetConnectionString("MongoDb"));
            var connectionString = "mongodb+srv://fatihdursunuzerofficial:im45xZpWunDNvd57@cluster0.gby0lnu.mongodb.net/?retryWrites=true&w=majority";
            client = new MongoClient(connectionString);
            stockDbContext = StockDbContext.Create(client.GetDatabase("Stocks"));
        }

        public List<TEntity> GetAll()
        {
            return stockDbContext.Set<TEntity>().ToList();
        }
        public async Task<TEntity> Update(TEntity entity)
        {
            stockDbContext.Attach(entity);
            stockDbContext.Entry(entity).State = EntityState.Modified;
            await stockDbContext.SaveChangesAsync();
            return entity;
        }
        public async Task<TEntity> Add(TEntity entity)
        {
            stockDbContext.Set<TEntity>().Add(entity);
            await stockDbContext.SaveChangesAsync();
            return entity;
        }

        /*public TEntity GetById(TEntityId id)
        {
            return stockDbContext.Set<TEntity>().FirstOrDefault(entity => entity.Id == id);
        }*/
    }
}
