namespace example.Entities
{
    public interface IRepository<T>
    {
        T Save(T t);
    }
}