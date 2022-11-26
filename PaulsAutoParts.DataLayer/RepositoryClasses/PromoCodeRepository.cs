using System;
using System.Collections.Generic;
using System.Linq;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;

namespace PaulsAutoParts.DataLayer
{
  public partial class PromoCodeRepository : IRepository<PromoCode, PromoCodeSearch>
  {
    #region Constructor
    public PromoCodeRepository(PaulsAutoPartsDbContext context)
    {
      _DbContext = context;
    }
    #endregion

    #region Protected Properties
    protected readonly PaulsAutoPartsDbContext _DbContext;
    #endregion

    #region Get Method
    public List<PromoCode> Get()
    {
      return _DbContext.PromoCodes.OrderBy(p => p.PromotionalCode).ToList();
    }
    #endregion

    #region Get(id) Method
    public PromoCode Get(int id)
    {
      return _DbContext.PromoCodes.Where(p => p.PromoCodeId == id).FirstOrDefault();
    }
    #endregion

    #region Get(code) Method
    public PromoCode Get(string code)
    {
      // Find Promo Code that is valid between the start and end date
      return _DbContext
        .PromoCodes.Where(p => p.PromotionalCode == code &&
               (p.StartDate.HasValue ? DateTime.Now >= p.StartDate : true) &&
               (p.EndDate.HasValue ? DateTime.Now <= p.EndDate : true)).FirstOrDefault();
    }
    #endregion

    #region Search Method
    public List<PromoCode> Search(PromoCodeSearch entity)
    {
      // Perform Searching
      return _DbContext.PromoCodes.Where(p =>
        (string.IsNullOrEmpty(entity.Code) ? true : p.PromotionalCode.StartsWith(entity.Code))).ToList();
    }
    #endregion

    #region CreateEmpty Method
    public virtual PromoCode CreateEmpty()
    {
      return new PromoCode
      {
        PromoCodeId = null
        ,
        PromotionalCode = null
        ,
        DiscountPercent = 0
        ,
        StartDate = null
        ,
        EndDate = null
      };
    }
    #endregion

    #region Insert Method
    public virtual PromoCode Insert(PromoCode entity)
    {
      // Add new entity to PromoCodes DbSet
      _DbContext.PromoCodes.Add(entity);

      // Save changes in database
      _DbContext.SaveChanges();

      return entity;
    }
    #endregion

    #region Update Method
    public virtual PromoCode Update(PromoCode entity)
    {
      // Update entity in PromoCodes DbSet
      _DbContext.PromoCodes.Update(entity);

      // Save changes in database
      _DbContext.SaveChanges();

      return entity;
    }
    #endregion

    #region Delete Method
    public virtual bool Delete(int id)
    {
      // Locate the entity to delete in the PromoCodes DbSet
      _DbContext.PromoCodes.Remove(_DbContext.PromoCodes.Find(id));

      // Save changes in database
      _DbContext.SaveChanges();

      return true;
    }
    #endregion
  }
}
