namespace PaulsAutoParts.Common
{
  public interface IRepositoryStringPK<TEntity, TSearch> : IRepositoryBase<TEntity, TSearch>
  {
    TEntity Get(string id);
    bool Delete(string id);
  }
}