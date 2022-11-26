using Microsoft.AspNetCore.Mvc;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;
using PaulsAutoParts.AppClasses;
using PaulsAutoParts.ViewModelLayer;

namespace PaulsAutoParts.Controllers
{
  public class CustomerMaintController : AppController
  {
    #region Constructor
    /// <summary>
    /// Constructor for CustomerMaintController class
    /// </summary>
    public CustomerMaintController(AppSession session,
             IRepository<Customer, CustomerSearch> repo) : base(session)
    {
      _repo = repo;
    }
    #endregion

    #region Private Fields
    private readonly IRepository<Customer, CustomerSearch> _repo;
    #endregion

    #region CustomerMaintIndex Methods
    /// <summary>
    /// Executes when browser issues a Get request for /Customer and 
    /// directs to the Index view
    /// </summary>
    /// <returns>An ActionResult that contains an instance of CustomerViewModel</returns>
    public ActionResult CustomerMaintIndex()
    {
      // Create view model and pass in repository
      CustomerViewModel vm = new(_repo);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Call method to load data
      vm.Get();

      return View(vm);
    }

    /// <summary>
    /// Handles the browser Post request for Customer form
    /// </summary>
    /// <param name="vm">A CustomerViewModel instance for the entry</param>
    /// <returns>An ActionResult that either navigates to the index or stays on this view and displays errors</returns>
    [HttpPost]
    public ActionResult CustomerMaintIndex(CustomerViewModel vm)
    {
      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Assign Repository to View Model
      vm.Repository = _repo;

      if (ModelState.IsValid) {
        // Save the Data
        vm.Save();

        // Redirect back to list
        return RedirectToAction("CustomerMaintIndex");
      }
      else {
        vm.IsDetailVisible = true;

        return View(vm);
      }
    }
    #endregion

    public ActionResult ViewOrders(int id, string firstName, string lastName)
    {
      // Set CustomerId into Session
      UserSession.CustomerId = id;
      // Lookup Customer Information
      Customer cust = _repo.Get(id);
      // Set Customer Information
      UserSession.CustomerName = cust.FirstName + " " + cust.LastName;
      // Inform Customer Payment Page we are coming from here
      UserSession.IsFromCustomerMaintenance = true;

      return RedirectToAction("CustomerPaymentIndex", "CustomerPayment");
    }

    #region Edit Method
    [HttpGet]
    public IActionResult Edit(int id)
    {
      // Create view model and pass in repository
      CustomerViewModel vm = new(_repo);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Call method to load a single record
      vm.Get(id);

      return View("CustomerMaintIndex", vm);
    }
    #endregion

    #region Search Method
    [HttpGet]
    public IActionResult Search(CustomerViewModel vm)
    {
      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Assign Repository to View Model
      vm.Repository = _repo;

      // Call method to search for data
      vm.Search();

      return View("CustomerMaintIndex", vm);
    }
    #endregion

    #region Add Method
    [HttpGet]
    public IActionResult Add()
    {
      // Create view model and pass in repository
      CustomerViewModel vm = new(_repo)
      {
        IsDetailVisible = true
      };

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Call method to create an empty entity to add
      vm.CreateEmptyEntity();

      return View("CustomerMaintIndex", vm);
    }
    #endregion

    #region Delete Method
    [HttpGet]
    public IActionResult Delete(int id)
    {
      // Create view model and pass in repository
      CustomerViewModel vm = new(_repo);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Call method to delete a record
      vm.Delete(id);

      return RedirectToAction("CustomerMaintIndex");
    }
    #endregion
  }
}
