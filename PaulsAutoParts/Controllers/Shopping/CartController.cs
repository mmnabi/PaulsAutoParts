using System;
using Microsoft.AspNetCore.Mvc;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;
using PaulsAutoParts.AppClasses;
using PaulsAutoParts.ViewModelLayer;

namespace PaulsAutoParts.Controllers
{
  public class CartController : AppController
  {
    #region Constructor
    public CartController(AppSession session,
             IRepository<Product, ProductSearch> repo,
             IRepository<VehicleType, VehicleTypeSearch> vrepo,
             IRepository<PromoCode, PromoCodeSearch> prepo) : base(session)
    {
      _repo = repo;
      _vehicleRepo = vrepo;
      _promoRepo = prepo;
    }
    #endregion

    #region Private Fields      
    private readonly IRepository<Product, ProductSearch> _repo;
    private readonly IRepository<VehicleType, VehicleTypeSearch> _vehicleRepo;
    private readonly IRepository<PromoCode, PromoCodeSearch> _promoRepo;
    #endregion

    [HttpGet]
    public IActionResult Index()
    {
      // Set Cart from Session
      ShoppingViewModel vm = new(_repo, _vehicleRepo, UserSession.Cart);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Set Cart into View Model
      vm.Cart = UserSession.Cart;

      // See if the cart has expired
      if (vm.Cart.DateExpires < DateTime.Now) {
        // Expire the cart
        vm.ExpireCart(vm.Cart);

        // Set cart into session
        UserSession.Cart = vm.Cart;
      }

      return View(vm);
    }

    [HttpPost]
    public IActionResult UpdateQuantity(ShoppingCartItem item)
    {
      // Set Cart from Session
      ShoppingViewModel vm = new(_repo, _vehicleRepo, UserSession.Cart);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Update item
      vm.UpdateQuantity(item);

      // Set cart into session
      UserSession.Cart = vm.Cart;

      // Must clear model state to have new quantity appear
      ModelState.Clear();

      return View("Index", vm);
    }

    [HttpPost]
    public IActionResult ApplyPromoCode(ShoppingViewModel vm)
    {
      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Assign Repositories
      vm.Repository = _repo;
      vm.VehicleRepository = _vehicleRepo;
      vm.PromoCodeRepository = _promoRepo;

      // Set Cart from Session
      vm.Cart = UserSession.Cart;

      // Apply Promo Code if it exists, and another one has not been applied
      vm.ApplyPromoCode();

      // Set cart into session
      UserSession.Cart = vm.Cart;

      // Must clear model state to have new values appear
      ModelState.Clear();

      return View("Index", vm);
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
      // Set Cart from Session
      ShoppingViewModel vm = new(_repo, _vehicleRepo, UserSession.Cart);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Set Cart into View Model
      vm.Cart = UserSession.Cart;

      // Remove item to cart
      vm.RemoveFromCart(vm.Cart, id, UserSession.CustomerId.Value);

      // Set cart into session
      UserSession.Cart = vm.Cart;

      // Must clear model state to have new values appear
      ModelState.Clear();

      return View("Index", vm);
    }
  }
}
