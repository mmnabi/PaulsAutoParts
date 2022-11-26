using Microsoft.AspNetCore.Mvc;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;
using PaulsAutoParts.AppClasses;
using PaulsAutoParts.ViewModelLayer;

namespace PaulsAutoParts.Controllers
{
  public class CustomerController : AppController
  {
    #region Constructor
    /// <summary>
    /// Constructor for CustomerController class
    /// </summary>
    public CustomerController(AppSession session,
           IRepository<Customer, CustomerSearch> repo) : base(session)
    {
      _repo = repo;
    }
    #endregion

    #region Private Fields
    private readonly IRepository<Customer, CustomerSearch> _repo;
    #endregion

    #region CustomerIndex Methods
    /// <summary>
    /// Executes when browser issues a Get request for /Customer and 
    /// directs to the Index view
    /// </summary>
    /// <returns>An ActionResult that contains an instance of CustomerViewModel</returns>
    public ActionResult CustomerIndex()
    {
      // Create view model and pass in repository
      CustomerViewModel vm = new(_repo);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Call method to load current customer data
      vm.Get(UserSession.CustomerId.Value);

      return View(vm);
    }

    /// <summary>
    /// Handles the browser Post request for Customer form
    /// </summary>
    /// <param name="vm">A CustomerViewModel instance for the entry</param>
    /// <returns>An ActionResult that either navigates to the index or stays on this view and displays errors</returns>
    [HttpPost]
    public ActionResult CustomerIndex(CustomerViewModel vm)
    {
      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Assign Repository to View Model
      vm.Repository = _repo;

      if (ModelState.IsValid) {
        // Save the Data
        vm.Save();

        vm.Message = "Your Information Has Been Updated";

        // Redisplay Data
        return View("CustomerIndex", vm);
      }
      else {
        vm.IsDetailVisible = true;

        return View(vm);
      }
    }
    #endregion
  }
}
