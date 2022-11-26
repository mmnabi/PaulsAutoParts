using Microsoft.AspNetCore.Mvc;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;
using PaulsAutoParts.AppClasses;
using PaulsAutoParts.ViewModelLayer;

namespace PaulsAutoParts.Controllers
{
  public class CheckOutController : AppController
  {
    #region Constructor
    public CheckOutController(AppSession session,
             IRepository<CustomerPayment, CustomerPaymentSearch> repository,
             IRepository<OrderHeader, OrderHeaderSearch> ohRepository,
             IRepository<OrderDetail, OrderDetailSearch> odRepository) : base(session)
    {
      _repo = repository;
      _headerRepo = ohRepository;
      _detailRepo = odRepository;
    }
    #endregion

    #region Private Fields      
    private readonly IRepository<CustomerPayment, CustomerPaymentSearch> _repo;
    private readonly IRepository<OrderHeader, OrderHeaderSearch> _headerRepo;
    private readonly IRepository<OrderDetail, OrderDetailSearch> _detailRepo;
    #endregion

    [HttpGet]
    public IActionResult Index()
    {
      // Set Cart from Session
      CheckOutViewModel vm = new(_repo, _headerRepo, _detailRepo, UserSession.Cart);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Set Customer Name into CC information
      vm.CustomerCreditCard.NameOnCard = vm.CustomerName;

      // Load Drop-Downs
      vm.LoadCardTypes();
      vm.LoadMonths();
      vm.LoadYears();

      return View(vm);
    }

    [HttpPost]
    public IActionResult ProcessCreditCard(CheckOutViewModel vm)
    {
      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Set Repositories into View Model
      vm.Repository = _repo;
      vm.OrderHeaderRepository = _headerRepo;
      vm.OrderDetailRepository = _detailRepo;

      // Set cart into view model
      vm.Cart = UserSession.Cart;

      // Process the credit card information
      vm.ProcessCreditCard(UserSession.CustomerId.Value);

      // Clear Shopping Cart
      vm.ExpireCart(vm.Cart);

      // Set cart back into session
      UserSession.Cart = vm.Cart;

      return View("Receipt", vm);
    }
  }
}
