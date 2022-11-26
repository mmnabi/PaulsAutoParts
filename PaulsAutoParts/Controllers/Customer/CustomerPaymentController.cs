using Microsoft.AspNetCore.Mvc;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;
using PaulsAutoParts.AppClasses;
using PaulsAutoParts.ViewModelLayer;

namespace PaulsAutoParts.Controllers
{
  public class CustomerPaymentController : AppController
  {
    #region Constructor
    /// <summary>
    /// Constructor for CustomerPaymentController class
    /// </summary>
    public CustomerPaymentController(AppSession session,
           IRepository<CustomerPayment, CustomerPaymentSearch> repo,
           IRepository<OrderHeader, OrderHeaderSearch> ohRepository,
           IRepository<OrderDetail, OrderDetailSearch> odRepository) : base(session)
    {
      _repo = repo;
      _headerRepo = ohRepository;
      _detailRepo = odRepository;
    }
    #endregion

    #region Private Fields
    private readonly IRepository<CustomerPayment, CustomerPaymentSearch> _repo;
    private readonly IRepository<OrderHeader, OrderHeaderSearch> _headerRepo;
    private readonly IRepository<OrderDetail, OrderDetailSearch> _detailRepo;
    #endregion

    #region CustomerPaymentIndex Method
    /// <summary>
    /// Executes when browser issues a Get request for /CustomerPayment and 
    /// directs to the Index view
    /// </summary>
    /// <returns>An ActionResult that contains an instance of CustomerPaymentViewModel</returns>
    public ActionResult CustomerPaymentIndex()
    {
      // Create view model and pass in repository
      CustomerPaymentViewModel vm = new(_repo);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Setup SearchEntity to get all payments for the current customer
      vm.SearchEntity.CustomerId = UserSession.CustomerId;

      // Called from Customer Maintenance?
      vm.IsFromCustomerMaintenance = UserSession.IsFromCustomerMaintenance;

      // Call method to load data
      vm.Search();

      return View(vm);
    }
    #endregion

    #region ViewDetail Method
    public ActionResult ViewDetail(int id)
    {
      // Create view model and pass in repository
      CustomerPaymentViewModel vm = new(_repo, _headerRepo, _detailRepo);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Setup SearchEntity to get all payments for the current customer
      vm.SearchEntity.CustomerId = UserSession.CustomerId;

      // Called from Customer Maintenance?
      vm.IsFromCustomerMaintenance = UserSession.IsFromCustomerMaintenance;

      // Call method to load data
      vm.Search();

      // Call method to get order information
      vm.GetOrderInformation(id);

      return View("CustomerPaymentIndex", vm);
    }
    #endregion
  }
}
