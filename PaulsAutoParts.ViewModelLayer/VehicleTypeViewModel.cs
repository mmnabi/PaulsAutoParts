using System;
using System.Collections.Generic;
using System.Linq;
using PaulsAutoParts.Common;
using PaulsAutoParts.DataLayer;
using PaulsAutoParts.EntityLayer;

namespace PaulsAutoParts.ViewModelLayer
{
  public class VehicleTypeViewModel : AppViewModelBase
  {
    #region Constructors
    /// <summary>
    ///  NOTE: You need to have a parameterless constructor for Post-Backs in MVC    
    /// </summary>
    public VehicleTypeViewModel() : base()
    {
      Init();
    }

    public VehicleTypeViewModel(IRepository<VehicleType, VehicleTypeSearch> repository) : base()
    {
      Init();

      Repository = repository;
    }
    #endregion

    #region Properties
    public IRepository<VehicleType, VehicleTypeSearch> Repository { get; set; }
    public List<VehicleType> DataCollection { get; set; }
    public VehicleType SelectedEntity { get; set; }
    public VehicleTypeSearch SearchEntity { get; set; }
    #endregion

    #region Init Method
    public override void Init()
    {
      base.Init();

      DataCollection = new List<VehicleType>();
      SelectedEntity = new VehicleType();
      SearchEntity = new VehicleTypeSearch();
    }
    #endregion

    #region GetYears Method
    public List<string> GetYears()
    {
      Years = ((VehicleTypeRepository)Repository).GetYears();

      if (Years.Count > 0) {
        Years.Insert(0, "<-- Select a Year -->");
        if (string.IsNullOrEmpty(SearchEntity.Year)) {
          SearchEntity.Year = Years[1];
        }
      }

      return Years;
    }
    #endregion

    #region Get Method
    public override void Get()
    {
      IsDetailVisible = false;

      if (Repository == null) {
        throw new ApplicationException("Must set the Repository property.");
      }
      else {
        DataCollection = Repository.Get();
      }

      if (DataCollection != null) {
        TotalRecords = DataCollection.Count;
      }
    }
    #endregion

    #region Get(id) Method
    public override void Get(int id)
    {
      IsDetailVisible = true;

      if (Repository == null) {
        throw new ApplicationException("Must set the Repository property.");
      }
      else {
        SelectedEntity = Repository.Get(id);
      }

      if (SelectedEntity != null) {
        TotalRecords = 1;
      }
    }
    #endregion

    #region Search Method
    public override void Search()
    {
      IsDetailVisible = false;

      if (Repository == null) {
        throw new ApplicationException("Must set the Repository property.");
      }
      else {
        DataCollection = Repository.Search(SearchEntity).OrderByDescending(p => p.Year).ToList();
      }

      if (DataCollection != null) {
        TotalRecords = DataCollection.Count;
      }
    }
    #endregion

    #region CreateEmptyEntity Method
    public override void CreateEmptyEntity()
    {
      SelectedEntity = Repository.CreateEmpty();
    }
    #endregion

    #region Save Method
    public override bool Save()
    {
      bool ret = false;

      if (Validate()) {
        if (SelectedEntity.VehicleTypeId.HasValue) {
          // Update the current entity
          Repository.Update(SelectedEntity);
        }
        else {
          // Add a new entity
          Repository.Insert(SelectedEntity);
        }
        ret = true;
      }

      return ret;
    }
    #endregion

    #region Validate Method
    public override bool Validate()
    {
      IsValid = false;
      Messages = new List<string>();

      // TODO: Validate Your Properties Here


      IsValid = (Messages.Count == 0);

      return IsValid;
    }
    #endregion

    #region Delete Method
    public override bool Delete(int id)
    {
      // Delete the entity by id
      Repository.Delete(id);

      return true;
    }
    #endregion
  }
}
