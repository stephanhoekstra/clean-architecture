using System;

namespace core.domain
{
    public interface IGateWay<T> 
    {
        void Save(T entity);
        T Get(Guid id);
    }
}