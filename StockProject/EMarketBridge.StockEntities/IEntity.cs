﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMarketBridge.StockEntities
{
    public interface IEntity<TEntityId>
    {
        TEntityId Id { get; set; }
    }
}
