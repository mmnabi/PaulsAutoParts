using System.Collections.Generic;
using System.Linq;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;

namespace PaulsAutoParts.DataLayer
{
  public partial class CustomerRepository : IRepository<Customer, CustomerSearch>
  {
    #region Constructor
    public CustomerRepository(PaulsAutoPartsDbContext context)
    {
      _DbContext = context;
    }
    #endregion

    #region Protected Properties
    protected readonly PaulsAutoPartsDbContext _DbContext;
    #endregion

    #region Get Method
    public List<Customer> Get()
    {
      return _DbContext.Customers.OrderBy(c => c.LastName).ToList();
    }
    #endregion

    #region Get(id) Method
    public Customer Get(int id)
    {
      return _DbContext.Customers.Where(p => p.CustomerId == id).FirstOrDefault();
    }
    #endregion

    #region Search Method
    public List<Customer> Search(CustomerSearch entity)
    {
      // Perform Searching
      return _DbContext.Customers.Where(c =>
        (string.IsNullOrEmpty(entity.LastName) ? true : c.LastName.StartsWith(entity.LastName))).ToList();
    }
    #endregion

    #region CreateEmpty Method
    public virtual Customer CreateEmpty()
    {
      return new Customer
      {
        CustomerId = null
        ,
        FirstName = null
        ,
        LastName = null
        ,
        EmailAddress = null
        ,
        Phone = null
      };
    }
    #endregion

    #region Insert Method
    public virtual Customer Insert(Customer entity)
    {
      // Add new entity to Customers DbSet
      _DbContext.Customers.Add(entity);

      // Save changes in database
      _DbContext.SaveChanges();

      return entity;
    }
    #endregion

    #region Update Method
    public virtual Customer Update(Customer entity)
    {
      // Update entity in Customers DbSet
      _DbContext.Customers.Update(entity);

      // Save changes in database
      _DbContext.SaveChanges();

      return entity;
    }
    #endregion

    #region Delete Method
    public virtual bool Delete(int id)
    {
      // Locate the entity to delete in the Customers DbSet
      _DbContext.Customers.Remove(_DbContext.Customers.Find(id));

      // Save changes in database
      _DbContext.SaveChanges();

      return true;
    }
    #endregion
  }
}
