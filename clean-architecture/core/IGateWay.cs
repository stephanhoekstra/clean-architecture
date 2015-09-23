using System;
using core.domain;

namespace core
{

    public interface IOrderGateWay : IGateWay<Order> { }

    public interface IGateWay<T> 
    {
        void Save(T entity);
        T Get(Guid id);
    }
}