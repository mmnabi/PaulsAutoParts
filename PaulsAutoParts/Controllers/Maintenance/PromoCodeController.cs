using Microsoft.AspNetCore.Mvc;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;
using PaulsAutoParts.AppClasses;
using PaulsAutoParts.ViewModelLayer;

namespace PaulsAutoParts.Controllers
{
  public class PromoCodeController : AppController
  {
    #region Constructor
    /// <summary>
    /// Constructor for PromoCodeController class
    /// </summary>
    public PromoCodeController(AppSession session,
           IRepository<PromoCode, PromoCodeSearch> repo) : base(session)
    {
      _repo = repo;
    }
    #endregion

    #region Private Fields
    private readonly IRepository<PromoCode, PromoCodeSearch> _repo;
    #endregion

    #region PromoCodeIndex Methods
    /// <summary>
    /// Executes when browser issues a Get request for /PromoCode and 
    /// directs to the Index view
    /// </summary>
    /// <returns>An ActionResult that contains an instance of PromoCodeViewModel</returns>
    public ActionResult PromoCodeIndex()
    {
      // Create view model and pass in repository
      PromoCodeViewModel vm = new(_repo);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Call method to load data
      vm.Get();

      return View(vm);
    }

    /// <summary>
    /// Handles the browser Post request for PromoCode form
    /// </summary>
    /// <param name="vm">A PromoCodeViewModel instance for the entry</param>
    /// <returns>An ActionResult that either navigates to the index or stays on this view and displays errors</returns>
    [HttpPost]
    public ActionResult PromoCodeIndex(PromoCodeViewModel vm)
    {
      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Assign Repository to View Model
      vm.Repository = _repo;

      if (ModelState.IsValid) {
        // Save the Data
        vm.Save();

        // Redirect back to list
        return RedirectToAction("PromoCodeIndex");
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
      PromoCodeViewModel vm = new(_repo);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Call method to load a single record
      vm.Get(id);

      return View("PromoCodeIndex", vm);
    }
    #endregion

    #region Search Method
    [HttpGet]
    public IActionResult Search(PromoCodeViewModel vm)
    {
      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Assign Repository to View Model
      vm.Repository = _repo;

      // Call method to search for data
      vm.Search();

      return View("PromoCodeIndex", vm);
    }
    #endregion

    #region Add Method
    [HttpGet]
    public IActionResult Add()
    {
      // Create view model and pass in repository
      PromoCodeViewModel vm = new(_repo)
      {
        IsDetailVisible = true
      };

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Call method to create an empty entity to add
      vm.CreateEmptyEntity();

      return View("PromoCodeIndex", vm);
    }
    #endregion

    #region Delete Method
    [HttpGet]
    public IActionResult Delete(int id)
    {
      // Create view model and pass in repository
      PromoCodeViewModel vm = new(_repo);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Call method to delete a record
      vm.Delete(id);

      return RedirectToAction("PromoCodeIndex");
    }
    #endregion
  }
}
