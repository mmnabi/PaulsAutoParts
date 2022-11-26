using System.Collections.Generic;

namespace PaulsAutoParts.Common
{
  public interface IRepositoryBase<TEntity, TSearch>
  {
    List<TEntity> Get();
    List<TEntity> Search(TSearch search);
    TEntity CreateEmpty();
    TEntity Insert(TEntity entity);
    TEntity Update(TEntity entity);
  }
}