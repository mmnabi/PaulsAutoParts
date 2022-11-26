using System;
using System.Collections.Generic;
using System.Linq;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;

namespace PaulsAutoParts.DataLayer
{
  public partial class ProductRepository : IRepository<Product, ProductSearch>
  {
    #region Constructor
    public ProductRepository(PaulsAutoPartsDbContext context)
    {
      _DbContext = context;
    }
    #endregion

    #region Protected Properties
    protected readonly PaulsAutoPartsDbContext _DbContext;
    #endregion

    #region Get Method
    public List<Product> Get()
    {
      return _DbContext.Products.OrderBy(p => p.ProductName).ToList();
    }
    #endregion

    #region Get(id) Method
    public Product Get(int id)
    {
      return _DbContext.Products.Where(p => p.ProductId == id).FirstOrDefault();
    }
    #endregion

    #region GetCategories Method
    public List<string> GetCategories()
    {
      return _DbContext.Products.Select(p => p.Category).Distinct().OrderBy(p => p).ToList();
    }
    #endregion

    #region Search Method
    public List<Product> Search(ProductSearch entity)
    {
      // Perform Searching
      return _DbContext.Products.Where(p =>
        (string.IsNullOrEmpty(entity.ProductName) ? true : p.ProductName.Contains(entity.ProductName)) &&
        (string.IsNullOrEmpty(entity.Category) ? true : p.Category.Contains(entity.Category))).ToList();
    }
    #endregion

    #region CreateEmpty Method
    public virtual Product CreateEmpty()
    {
      return new Product
      {
        ProductId = null
        ,
        ProductName = null
        ,
        Category = null
        ,
        Price = 0
      };
    }
    #endregion

    #region Insert Method
    public virtual Product Insert(Product entity)
    {
      // Add new entity to Products DbSet
      _DbContext.Products.Add(entity);

      // Save changes in database
      _DbContext.SaveChanges();

      return entity;
    }
    #endregion

    #region Update Method
    public virtual Product Update(Product entity)
    {
      // Update entity in Products DbSet
      _DbContext.Products.Update(entity);

      // Save changes in database
      _DbContext.SaveChanges();

      return entity;
    }
    #endregion

    #region Delete Method
    public virtual bool Delete(int id)
    {
      // Locate the entity to delete in the Products DbSet
      _DbContext.Products.Remove(_DbContext.Products.Find(id));

      // Save changes in database
      _DbContext.SaveChanges();

      return true;
    }
    #endregion
  }
}
