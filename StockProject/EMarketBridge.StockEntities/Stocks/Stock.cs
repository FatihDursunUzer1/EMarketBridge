using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EMarketBridge.StockEntities.Stocks
{
    public class Stock:IEntity<ObjectId>
    {
        public ObjectId Id { get; set; } = ObjectId.GenerateNewId();
        public string ProductId { get; set; }
        public int Count { get; set; }
    }
}
