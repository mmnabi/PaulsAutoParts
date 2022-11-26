using System.Collections.Generic;
using System.Linq;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;

namespace PaulsAutoParts.DataLayer
{
  public partial class OrderHeaderRepository : IRepository<OrderHeader, OrderHeaderSearch>
  {
    #region Constructor
    public OrderHeaderRepository(PaulsAutoPartsDbContext context)
    {
      _DbContext = context;
    }
    #endregion

    #region Protected Properties
    protected readonly PaulsAutoPartsDbContext _DbContext;
    #endregion

    #region Get Method
    public List<OrderHeader> Get()
    {
      return _DbContext.OrderHeaders.OrderByDescending(h => h.OrderDate).ToList();
    }
    #endregion

    #region Get(id) Method
    public OrderHeader Get(int id)
    {
      return _DbContext.OrderHeaders.Where(p => p.OrderHeaderId == id).FirstOrDefault();
    }
    #endregion

    #region Search Method
    public List<OrderHeader> Search(OrderHeaderSearch entity)
    {
      // TODO: Create EF Where Clause
      // Perform Searching
      // return _DbContext.OrderHeaders.Where(x =>
      //   (string.IsNullOrEmpty(entity.Name) ? true : x.Name.StartsWith(entity.Name)) &&
      //   (x.ListPrice >= entity.ListPrice)).ToList();

      return _DbContext.OrderHeaders.ToList();
    }
    #endregion

    #region CreateEmpty Method
    public virtual OrderHeader CreateEmpty()
    {
      return new OrderHeader
      {
        OrderHeaderId = null
        ,
        CustomerId = null
        ,
        OrderDate = null
        ,
        PromotionalCode = null
      };
    }
    #endregion

    #region Insert Method
    public virtual OrderHeader Insert(OrderHeader entity)
    {
      // Add new entity to OrderHeaders DbSet
      _DbContext.OrderHeaders.Add(entity);

      // Save changes in database
      _DbContext.SaveChanges();

      return entity;
    }
    #endregion

    #region Update Method
    public virtual OrderHeader Update(OrderHeader entity)
    {
      // Update entity in OrderHeaders DbSet
      _DbContext.OrderHeaders.Update(entity);

      // Save changes in database
      _DbContext.SaveChanges();

      return entity;
    }
    #endregion

    #region Delete Method
    public virtual bool Delete(int id)
    {
      // Locate the entity to delete in the OrderHeaders DbSet
      _DbContext.OrderHeaders.Remove(_DbContext.OrderHeaders.Find(id));

      // Save changes in database
      _DbContext.SaveChanges();

      return true;
    }
    #endregion
  }
}
