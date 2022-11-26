using System.Collections.Generic;
using System.Linq;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;

namespace PaulsAutoParts.DataLayer
{
  public partial class OrderDetailRepository : IRepository<OrderDetail, OrderDetailSearch>
  {
    #region Constructor
    public OrderDetailRepository(PaulsAutoPartsDbContext context)
    {
      _DbContext = context;
    }
    #endregion

    #region Protected Properties
    protected readonly PaulsAutoPartsDbContext _DbContext;
    #endregion

    #region Get Method
    public List<OrderDetail> Get()
    {
      return _DbContext.OrderDetails.OrderBy(d => d.ProductName).ToList();
    }
    #endregion

    #region Get(id) Method
    public OrderDetail Get(int id)
    {
      return _DbContext.OrderDetails.Where(p => p.OrderDetailId == id).FirstOrDefault();
    }
    #endregion

    #region Search Method
    public List<OrderDetail> Search(OrderDetailSearch entity)
    {
      // Perform Searching
      return _DbContext.OrderDetails.Where(d =>
        (string.IsNullOrEmpty(entity.ProductName) ? true : d.ProductName.StartsWith(entity.ProductName)) &&
        (d.OrderHeaderId.HasValue ? d.OrderHeaderId == entity.OrderHeaderId : true)).ToList();
    }
    #endregion

    #region CreateEmpty Method
    public virtual OrderDetail CreateEmpty()
    {
      return new OrderDetail
      {
        OrderDetailId = null
        ,
        CustomerId = null
        ,
        OrderHeaderId = null
        ,
        ProductId = null
        ,
        Quantity = null
        ,
        ProductName = null
        ,
        Price = null
      };
    }
    #endregion

    #region Insert Method
    public virtual OrderDetail Insert(OrderDetail entity)
    {
      // Add new entity to OrderDetails DbSet
      _DbContext.OrderDetails.Add(entity);

      // Save changes in database
      _DbContext.SaveChanges();

      return entity;
    }
    #endregion

    #region Update Method
    public virtual OrderDetail Update(OrderDetail entity)
    {
      // Update entity in OrderDetails DbSet
      _DbContext.OrderDetails.Update(entity);

      // Save changes in database
      _DbContext.SaveChanges();

      return entity;
    }
    #endregion

    #region Delete Method
    public virtual bool Delete(int id)
    {
      // Locate the entity to delete in the OrderDetails DbSet
      _DbContext.OrderDetails.Remove(_DbContext.OrderDetails.Find(id));

      // Save changes in database
      _DbContext.SaveChanges();

      return true;
    }
    #endregion
  }
}
