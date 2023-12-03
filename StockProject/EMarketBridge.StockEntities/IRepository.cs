using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.StockEntities
{
    public interface IRepository<TEntity,TEntityId>
    {
        List<TEntity> GetAll();
        Task<TEntity> Update(TEntity entity);
        Task<TEntity> Add(TEntity entity);
        //TEntity GetById(TEntityId id);

    }
}
