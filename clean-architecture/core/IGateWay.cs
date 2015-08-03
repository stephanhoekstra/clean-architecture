using System;

namespace core
{
    public interface IGateWay<T> 
    {
        void Save(T entity);
        T Get(Guid id);
    }
}