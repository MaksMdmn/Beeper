namespace WebCalculator.Domain.Repositories.Interfaces
{
    public interface IBasicRepository<TModel, TKey>
        where TModel : class
    {
        TModel Get(TKey id);
        void Create(TModel model);
        void Update(TModel model);
        void Delete(TKey id);
    }
}
