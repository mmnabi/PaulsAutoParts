using Microsoft.AspNetCore.Mvc;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;
using PaulsAutoParts.AppClasses;
using PaulsAutoParts.ViewModelLayer;

namespace PaulsAutoParts.Controllers
{
  public class ShoppingController : AppController
  {
    #region Constructor
    public ShoppingController(AppSession session,
             IRepository<Product, ProductSearch> repo,
             IRepository<VehicleType, VehicleTypeSearch> vrepo) : base(session)
    {
      _repo = repo;
      _vehicleRepo = vrepo;
    }
    #endregion

    #region Private Fields      
    private readonly IRepository<Product, ProductSearch> _repo;
    private readonly IRepository<VehicleType, VehicleTypeSearch> _vehicleRepo;
    #endregion

    #region Index Method
    [HttpGet]
    public IActionResult Index()
    {
      // Set Cart from Session
      ShoppingViewModel vm = new(_repo, _vehicleRepo, UserSession.Cart);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Load Drop-Downs
      vm.GetYears();
      vm.GetCategories();

      return View(vm);
    }
    #endregion

    #region ChangeYearMakeModel Method
    [HttpGet]
    public IActionResult ChangeYearMakeModel(ShoppingViewModel vm)
    {
      // Set values for changing year/make/model
      SetYearMakeModel(vm);

      return View("Index", vm);
    }
    #endregion

    #region SearchYearMakeModel Method
    [HttpGet]
    public IActionResult SearchYearMakeModel(ShoppingViewModel vm)
    {
      // Set values for searching
      SetYearMakeModel(vm);

      // Search for Products
      vm.Search();

      return View("Index", vm);
    }
    #endregion

    #region SetYearMakeModel Method
    protected void SetYearMakeModel(ShoppingViewModel vm)
    {
      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Assign Repositories
      vm.Repository = _repo;
      vm.VehicleRepository = _vehicleRepo;

      // Reset Search Entity
      vm.SearchEntity.ProductName = null;

      // Set Cart from Session
      vm.Cart = UserSession.Cart;

      // Load Drop-Downs
      vm.GetCategories();
      vm.LoadYearsMakesModels();

      // Store last product search
      UserSession.LastProductSearch = vm.SearchEntity;
    }
    #endregion

    #region SearchNameCategory Method
    [HttpGet]
    public IActionResult SearchNameCategory(ShoppingViewModel vm)
    {
      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Assign Repositories
      vm.Repository = _repo;
      vm.VehicleRepository = _vehicleRepo;

      // Reset Search Entity
      vm.SearchEntity.Year = null;
      vm.SearchEntity.Make = null;
      vm.SearchEntity.Model = null;

      // Set Cart from Session
      vm.Cart = UserSession.Cart;

      // Load Drop-Downs
      vm.GetYears();
      vm.GetCategories();

      // Store last product search
      UserSession.LastProductSearch = vm.SearchEntity;

      // Search for Products
      vm.Search();

      return View("Index", vm);
    }
    #endregion

    #region Add Method
    [HttpGet]
    public IActionResult Add(int id)
    {
      // Set Cart from Session
      ShoppingViewModel vm = new(_repo, _vehicleRepo, UserSession.Cart);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Restore last product search
      vm.SearchEntity = UserSession.LastProductSearch;

      // Add item to cart
      vm.AddToCart(id, UserSession.CustomerId.Value);

      // Set cart into session
      UserSession.Cart = vm.Cart;

      // Load Drop-Downs
      vm.LoadYearsMakesModels();
      vm.GetCategories();

      // Search for Products
      vm.Search();

      return View("Index", vm);
    }
    #endregion

    #region Remove Method
    [HttpGet]
    public IActionResult Remove(int id)
    {
      // Set Cart from Session
      ShoppingViewModel vm = new(_repo, _vehicleRepo, UserSession.Cart);

      // Set "Common" View Model Properties from Session
      base.SetViewModelFromSession(vm, UserSession);

      // Restore last product search
      vm.SearchEntity = UserSession.LastProductSearch;

      // Remove item to cart
      vm.RemoveFromCart(vm.Cart, id, UserSession.CustomerId.Value);

      // Set cart into session
      UserSession.Cart = vm.Cart;

      // Load Drop-Downs
      vm.LoadYearsMakesModels();
      vm.GetCategories();

      // Search for Products
      vm.Search();

      return View("Index", vm);
    }
    #endregion
  }
}
