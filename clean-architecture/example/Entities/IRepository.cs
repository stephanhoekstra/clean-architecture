namespace example.Entities
{
    public interface IRepository< TId, TEntity>
    {
        TEntity Save(TEntity entity);
        TEntity Get(TId id);
    }
}