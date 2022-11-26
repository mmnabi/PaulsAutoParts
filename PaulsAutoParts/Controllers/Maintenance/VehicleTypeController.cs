using Microsoft.AspNetCore.Mvc;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;
using PaulsAutoParts.AppClasses;
using PaulsAutoParts.ViewModelLayer;

namespace PaulsAutoParts.Controllers
{
  public class VehicleTypeController : AppController
  {
    #region Constructor
    /// <summary>
    /// Constructor for VehicleTypeController class
    /// </summary>
    public VehicleTypeController(AppSession session,
           IRepository<VehicleType, VehicleTypeSearch> repo) : base(session)
    {
      _repo = repo;
    }
    #endregion

    #region Private Fields
    private readonly IRepository<VehicleType, VehicleTypeSearch> _repo;
    #endregion

    #region VehicleTypeIndex Methods
    /// <summary>
    /// Executes when browser issues a Get request for /VehicleType and 
    /// directs to the Index view
    /// </summary>
    /// <returns>An ActionResult that contains an instance of VehicleTypeViewModel</returns>
    public ActionResult VehicleTypeIndex()
    {
      // Create view model and pass in repository
      VehicleTypeViewModel vm = new(_repo);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Setup search
      vm.GetYears();
      // Set default year to null
      vm.SearchEntity.Year = null;

      // Call method to search for data
      vm.Search();

      return View(vm);
    }

    /// <summary>
    /// Handles the browser Post request for VehicleType form
    /// </summary>
    /// <param name="vm">A VehicleTypeViewModel instance for the entry</param>
    /// <returns>An ActionResult that either navigates to the index or stays on this view and displays errors</returns>
    [HttpPost]
    public ActionResult VehicleTypeIndex(VehicleTypeViewModel vm)
    {
      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Assign Repository to View Model
      vm.Repository = _repo;

      if (ModelState.IsValid) {
        // Save the Data
        vm.Save();

        // Redirect back to list
        return RedirectToAction("VehicleTypeIndex");
      }
      else {
        vm.IsDetailVisible = true;

        return View(vm);
      }
    }
    #endregion

    #region Edit Method
    [HttpGet]
    public IActionResult Edit(int id)
    {
      // Create view model and pass in repository
      VehicleTypeViewModel vm = new(_repo);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Call method to load a single record
      vm.Get(id);

      return View("VehicleTypeIndex", vm);
    }
    #endregion

    #region Search Method
    [HttpGet]
    public IActionResult Search(VehicleTypeViewModel vm)
    {
      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Assign Repository to View Model
      vm.Repository = _repo;

      // Setup search
      vm.GetYears();

      // Call method to search for data
      vm.Search();

      return View("VehicleTypeIndex", vm);
    }
    #endregion

    #region Add Method
    [HttpGet]
    public IActionResult Add()
    {
      // Create view model and pass in repository
      VehicleTypeViewModel vm = new(_repo)
      {
        IsDetailVisible = true
      };

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Call method to create an empty entity to add
      vm.CreateEmptyEntity();

      return View("VehicleTypeIndex", vm);
    }
    #endregion

    #region Delete Method
    [HttpGet]
    public IActionResult Delete(int id)
    {
      // Create view model and pass in repository
      VehicleTypeViewModel vm = new(_repo);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Call method to delete a record
      vm.Delete(id);

      return RedirectToAction("VehicleTypeIndex");
    }
    #endregion
  }
}
