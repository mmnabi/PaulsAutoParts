using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PaulsAutoParts.Common;
using PaulsAutoParts.DataLayer;
using PaulsAutoParts.EntityLayer;

namespace PaulsAutoParts.ViewModelLayer
{
  public class ShoppingViewModel : AppViewModelBase
  {
    #region Constructors
    /// <summary>
    ///  NOTE: You need to have a parameterless constructor for Post-Backs in MVC    
    /// </summary>
    public ShoppingViewModel() : base()
    {
      Init();
    }

    public ShoppingViewModel(IRepository<Product, ProductSearch> repository) : base()
    {
      Init();

      Repository = repository;
    }

    public ShoppingViewModel(IRepository<Product, ProductSearch> repository, IRepository<VehicleType, VehicleTypeSearch> vrepo) : base()
    {
      Init();

      Repository = repository;
      VehicleRepository = vrepo;
    }

    public ShoppingViewModel(IRepository<Product, ProductSearch> repository, IRepository<VehicleType, VehicleTypeSearch> vrepo, ShoppingCart cart) : base()
    {
      Init();

      Repository = repository;
      VehicleRepository = vrepo;
      Cart = cart;
    }

    public ShoppingViewModel(IRepository<Product, ProductSearch> repository, IRepository<VehicleType, VehicleTypeSearch> vrepo, ShoppingCart cart, IRepository<PromoCode, PromoCodeSearch> prepo) : base()
    {
      Init();

      Repository = repository;
      VehicleRepository = vrepo;
      PromoCodeRepository = prepo;
      Cart = cart;
    }
    #endregion

    #region Properties
    public IRepository<Product, ProductSearch> Repository { get; set; }
    public IRepository<VehicleType, VehicleTypeSearch> VehicleRepository { get; set; }
    public IRepository<PromoCode, PromoCodeSearch> PromoCodeRepository { get; set; }
    public List<Product> DataCollection { get; set; }
    public Product SelectedEntity { get; set; }
    public ProductSearch SearchEntity { get; set; }
    public ShoppingCart Cart { get; set; }

    [Display(Name = "Promotional Code To Apply")]
    public string PromoCodeToApply { get; set; }

    public string SearchYearMakeModelCollapse { get; set; }
    public string SearchNameCategoryCollapse { get; set; }
    #endregion

    #region Init Method
    public override void Init()
    {
      base.Init();

      DataCollection = new List<Product>();
      SelectedEntity = new Product();
      SearchEntity = new ProductSearch();
      Cart = new ShoppingCart();

      PromoCodeToApply = string.Empty;

      SearchYearMakeModelCollapse = "collapse show";
      SearchNameCategoryCollapse = "collapse";
    }
    #endregion

    #region LoadYearsMakesModels Method
    public void LoadYearsMakesModels()
    {
      GetYears();

      if (int.TryParse(SearchEntity.Year, out int tmpYear)) {
        GetMakes(tmpYear);
      }

      if (int.TryParse(SearchEntity.Year, out tmpYear) && !string.IsNullOrEmpty(SearchEntity.Make)) {
        if (!SearchEntity.Make.StartsWith("<--")) {
          GetModels(tmpYear, SearchEntity.Make);
        }
      }
    }
    #endregion

    #region GetYears Method
    public List<string> GetYears()
    {
      Years = ((VehicleTypeRepository)VehicleRepository).GetYears();

      if (Years.Count > 0) {
        Years.Insert(0, "<-- Select a Year -->");
      }

      return Years;
    }
    #endregion

    #region GetMakes Method
    public List<string> GetMakes(int year)
    {
      Makes = ((VehicleTypeRepository)VehicleRepository).GetMakes(year);

      if (Makes.Count > 0) {
        Makes.Insert(0, "<-- Select a Make -->");
      }

      return Makes;
    }
    #endregion

    #region GetModels Method
    public List<string> GetModels(int year, string make)
    {
      Models = ((VehicleTypeRepository)VehicleRepository).GetModels(year, make);
      return Models;
    }
    #endregion

    #region GetCategories Method
    public List<string> GetCategories()
    {
      Categories = ((ProductRepository)Repository).GetCategories();

      if (Categories.Count > 0) {
        Categories.Insert(0, "<-- Select a Category -->");
      }

      return Categories;
    }
    #endregion

    #region Search Method
    public override void Search()
    {
      IsDetailVisible = false;

      if (!string.IsNullOrEmpty(SearchEntity.Year) && SearchEntity.Year.StartsWith("<--")) {
        SearchEntity.Year = null;
      }
      if (!string.IsNullOrEmpty(SearchEntity.Make) && SearchEntity.Make.StartsWith("<--")) {
        SearchEntity.Make = null;
      }
      if (!string.IsNullOrEmpty(SearchEntity.Model) && SearchEntity.Model.StartsWith("<--")) {
        SearchEntity.Model = null;
      }
      if (!string.IsNullOrEmpty(SearchEntity.Category) && SearchEntity.Category.StartsWith("<--")) {
        SearchEntity.Category = null;
      }

      if (Repository == null) {
        throw new ApplicationException("Must set the Repository property.");
      }
      else {
        DataCollection = Repository.Search(SearchEntity).OrderBy(p => p.ProductName).ToList();
      }

      // Find out which products are in the cart already
      if (Cart.Items.Count > 0) {
        foreach (Product item in DataCollection) {
          // Using .Any() because performance is better than .Count()
          if (Cart.Items.Where(ci => ci.ProductId == item.ProductId).Any()) {
            item.IsInCart = true;
          }
        }
      }

      if (DataCollection != null) {
        TotalRecords = DataCollection.Count;
      }
    }
    #endregion

    #region AddToCart Method
    public void AddToCart(int id, int custId)
    {
      if (Repository == null) {
        throw new ApplicationException("Must set the Repository property.");
      }
      else {
        // Look up product to get data
        SelectedEntity = Repository.Get(id);
        if (SelectedEntity != null) {
          // Create Cart
          Cart.DateCreated = DateTime.Now;
          Cart.Items.Add(new ShoppingCartItem
          {
            CustomerId = custId,
            DateAdded = DateTime.Now,
            ProductId = id,
            ProductName = SelectedEntity.ProductName,
            Price = SelectedEntity.Price
          });
        }
      }
    }
    #endregion

    #region UpdateQuantity Method
    public void UpdateQuantity(ShoppingCartItem item)
    {
      // Find shopping cart item
      ShoppingCartItem ci = Cart.Items.Find(c => c.ProductId == item.ProductId);
      if (ci != null) {
        // Update the quantity
        ci.Quantity = item.Quantity;
      }
    }
    #endregion

    #region ApplyPromoCode Method
    public string ApplyPromoCode()
    {
      // Make sure a promo code has not already been applied
      if (string.IsNullOrEmpty(Cart.PromotionCode.PromotionalCode)) {
        if (PromoCodeRepository == null) {
          throw new ApplicationException("Must set the PromoCodeRepository property.");
        }
        else {
          // See if promotion code exists
          PromoCode pcode = ((PromoCodeRepository)PromoCodeRepository).Get(PromoCodeToApply);
          if (pcode != null) {
            // Assign Promotion Code
            Cart.PromotionCode = pcode;

            // Apply Discount to Each Line Item
            foreach (ShoppingCartItem item in Cart.Items) {
              item.Price -= item.Price * pcode.DiscountPercent;
            }

            Message = "Promotional Code Applied.";
          }
          else {
            Message = $"Promotional Code {PromoCodeToApply} Does Not Exist.";
          }
        }
      }
      else {
        Message = "Another Promotional Code Has Already Been Applied.";
      }

      return Message;
    }
    #endregion
  }
}
