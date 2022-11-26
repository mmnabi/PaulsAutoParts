using System;
using System.Collections.Generic;
using System.Linq;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;

namespace PaulsAutoParts.DataLayer
{
  public partial class VehicleTypeRepository : IRepository<VehicleType, VehicleTypeSearch>
  {
    #region Constructor
    public VehicleTypeRepository(PaulsAutoPartsDbContext context)
    {
      _DbContext = context;
    }
    #endregion

    #region Protected Properties
    protected readonly PaulsAutoPartsDbContext _DbContext;
    #endregion

    #region Get Method
    public List<VehicleType> Get()
    {
      return _DbContext.VehicleTypes.OrderBy(v => v.Make).ThenBy(v => v.Model).ThenBy(v => v.Year).ToList();
    }
    #endregion

    #region Get(id) Method
    public VehicleType Get(int id)
    {
      return _DbContext.VehicleTypes.Where(p => p.VehicleTypeId == id).FirstOrDefault();
    }
    #endregion

    #region GetYears Method
    public List<string> GetYears()
    {
      return _DbContext.VehicleTypes.Select(v => v.Year.ToString()).Distinct().OrderByDescending(v => v).ToList();
    }
    #endregion

    #region GetMakes Method
    public List<string> GetMakes(int year)
    {
      return _DbContext.VehicleTypes.Where(v => v.Year == year).Select(v => v.Make).Distinct().OrderBy(v => v).ToList();
    }
    #endregion

    #region GetModels Method
    public List<string> GetModels(int year, string make)
    {
      return _DbContext.VehicleTypes.Where(v => v.Year == year && v.Make == make).Select(v => v.Model).Distinct().OrderBy(v => v).ToList();
    }
    #endregion

    #region Search Method
    public List<VehicleType> Search(VehicleTypeSearch entity)
    {
      if(entity.Year != null && entity.Year.StartsWith("<--")) {
        entity.Year = null;
      }

      // Perform Searching
      return _DbContext.VehicleTypes.Where(v =>
        (string.IsNullOrEmpty(entity.Make) ? true : v.Make.StartsWith(entity.Make)) &&
        (string.IsNullOrEmpty(entity.Model) ? true : v.Make.StartsWith(entity.Model)) &&
        (string.IsNullOrEmpty(entity.Year) ? true : v.Year == Convert.ToInt32(entity.Year))).ToList();
    }
    #endregion

    #region CreateEmpty Method
    public virtual VehicleType CreateEmpty()
    {
      return new VehicleType
      {
        VehicleTypeId = null,
        Year = DateTime.Now.Year,
        Make = null,
        Model = null
      };
    }
    #endregion

    #region Insert Method
    public virtual VehicleType Insert(VehicleType entity)
    {
      // Add new entity to VehicleTypes DbSet
      _DbContext.VehicleTypes.Add(entity);

      // Save changes in database
      _DbContext.SaveChanges();

      return entity;
    }
    #endregion

    #region Update Method
    public virtual VehicleType Update(VehicleType entity)
    {
      // Update entity in VehicleTypes DbSet
      _DbContext.VehicleTypes.Update(entity);

      // Save changes in database
      _DbContext.SaveChanges();

      return entity;
    }
    #endregion

    #region Delete Method
    public virtual bool Delete(int id)
    {
      // Locate the entity to delete in the VehicleTypes DbSet
      _DbContext.VehicleTypes.Remove(_DbContext.VehicleTypes.Find(id));

      // Save changes in database
      _DbContext.SaveChanges();

      return true;
    }
    #endregion
  }
}
