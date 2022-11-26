using System.Collections.Generic;
using System.Linq;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;

namespace PaulsAutoParts.DataLayer
{
  public partial class CustomerPaymentRepository : IRepository<CustomerPayment, CustomerPaymentSearch>
  {
    #region Constructor
    public CustomerPaymentRepository(PaulsAutoPartsDbContext context)
    {
      _DbContext = context;
    }
    #endregion

    #region Protected Properties
    protected readonly PaulsAutoPartsDbContext _DbContext;
    #endregion

    #region Get Method
    public List<CustomerPayment> Get()
    {
      return _DbContext.CustomerPayments.OrderBy(p => p.CustomerId).ToList();
    }
    #endregion

    #region Get(id) Method
    public CustomerPayment Get(int id)
    {
      return _DbContext.CustomerPayments.Where(p => p.CustomerPaymentId == id).FirstOrDefault();
    }
    #endregion

    #region Search Method
    public List<CustomerPayment> Search(CustomerPaymentSearch entity)
    {
      // Perform Searching
      return _DbContext.CustomerPayments.Where(p =>
        (entity.CustomerId.HasValue ? p.CustomerId == entity.CustomerId.Value : true) &&
        (entity.OrderHeaderId.HasValue ? p.OrderHeaderId == entity.OrderHeaderId.Value : true)).ToList();
    }
    #endregion

    #region CreateEmpty Method
    public virtual CustomerPayment CreateEmpty()
    {
      return new CustomerPayment
      {
        CustomerPaymentId = null
        ,
        CustomerId = null
        ,
        CCType = null
        ,
        CCAuth = null
        ,
        Response = null
        ,
        AmountPaid = null
      };
    }
    #endregion

    #region Insert Method
    public virtual CustomerPayment Insert(CustomerPayment entity)
    {
      // Add new entity to CustomerPayments DbSet
      _DbContext.CustomerPayments.Add(entity);

      // Save changes in database
      _DbContext.SaveChanges();

      return entity;
    }
    #endregion

    #region Update Method
    public virtual CustomerPayment Update(CustomerPayment entity)
    {
      // Update entity in CustomerPayments DbSet
      _DbContext.CustomerPayments.Update(entity);

      // Save changes in database
      _DbContext.SaveChanges();

      return entity;
    }
    #endregion

    #region Delete Method
    public virtual bool Delete(int id)
    {
      // Locate the entity to delete in the CustomerPayments DbSet
      _DbContext.CustomerPayments.Remove(_DbContext.CustomerPayments.Find(id));

      // Save changes in database
      _DbContext.SaveChanges();

      return true;
    }
    #endregion
  }
}
