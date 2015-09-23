using System;
using System.Collections.Generic;
using core;
using core.domain;

namespace gateways
{
    /// <summary>
    /// stores orders in memory only. 
    /// </summary>
    public class OrderGateWay: IOrderGateWay
    {
        private readonly Dictionary<Guid, Order> _orders;

        public OrderGateWay()
        {
            _orders = new Dictionary<Guid, Order>();
        }

        public void Save(Order entity)
        {
            entity.Id = Guid.NewGuid();
            _orders.Add(entity.Id,entity);
        }

        public Order Get(Guid id)
        {
            if (_orders.ContainsKey(id)) return _orders[id];
            return null;
        }
    }
}
