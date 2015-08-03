using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using core.domain;

namespace gateways
{
    public class InMemoryOrderGateWay:IGateWay<Order>
    {
        private readonly Dictionary<Guid, Order> _orders;

        public InMemoryOrderGateWay()
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
