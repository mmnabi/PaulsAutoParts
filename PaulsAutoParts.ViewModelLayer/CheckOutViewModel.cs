using System;
using System.Collections.Generic;
using System.Globalization;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;

namespace PaulsAutoParts.ViewModelLayer
{
  public class CheckOutViewModel : AppViewModelBase
  {
    #region Constructors
    /// <summary>
    ///  NOTE: You need to have a parameterless constructor for Post-Backs in MVC    
    /// </summary>
    public CheckOutViewModel() : base()
    {
      Init();
    }

    public CheckOutViewModel(IRepository<CustomerPayment, CustomerPaymentSearch> repository,
      IRepository<OrderHeader, OrderHeaderSearch> ohRepository,
      IRepository<OrderDetail, OrderDetailSearch> odRepository,
      ShoppingCart cart) : base()
    {
      Init();

      Repository = repository;
      OrderHeaderRepository = ohRepository;
      OrderDetailRepository = odRepository;
      Cart = cart;
    }
    #endregion

    #region Properties
    public IRepository<CustomerPayment, CustomerPaymentSearch> Repository { get; set; }
    public IRepository<OrderHeader, OrderHeaderSearch> OrderHeaderRepository { get; set; }
    public IRepository<OrderDetail, OrderDetailSearch> OrderDetailRepository { get; set; }
    public CustomerPayment SelectedEntity { get; set; }

    public ShoppingCart Cart { get; set; }
    public CreditCard CustomerCreditCard { get; set; }
    public OrderHeader OrderInformation { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }
    public CustomerPayment PaymentInformation { get; set; }

    public string DefaultLanguage { get; set; }
    public string Language { get; set; }

    public List<string> CardTypes { get; set; }
    public List<MonthInfo> Months { get; set; }
    #endregion

    #region Init Method
    public override void Init()
    {
      base.Init();

      Language = "en-us";
      DefaultLanguage = "en-us";

      SelectedEntity = new CustomerPayment();
      Cart = new ShoppingCart();

      CustomerCreditCard = new CreditCard
      {
        CardType = "Visa",
        CardNumber = "4024007103939509",
        NameOnCard = string.Empty,
        SecurityCode = "011",
        ExpMonth = DateTime.Now.Month,
        ExpYear = DateTime.Now.AddYears(1).Year,
        BillingPostalCode = "37211"
      };

      OrderInformation = new OrderHeader();
      OrderDetails = new List<OrderDetail>();
      PaymentInformation = new CustomerPayment();

      CardTypes = new List<string>();
      Months = new List<MonthInfo>();
    }
    #endregion

    #region LoadCardTypes Method
    public void LoadCardTypes()
    {
      CardTypes.Add("Visa");
      CardTypes.Add("MasterCard");
      CardTypes.Add("Discover");
      CardTypes.Add("American Express");
    }
    #endregion

    #region LoadMonths Method
    public void LoadMonths()
    {
      string[] monthNames;

      try {
        // Try to get month names
        monthNames = (new CultureInfo(Language)).DateTimeFormat.MonthNames;
      }
      catch (CultureNotFoundException) {
        // Default to a known language
        monthNames = (new System.Globalization.CultureInfo(DefaultLanguage)).DateTimeFormat.MonthNames;
      }

      // Create Months Array
      for (int index = 0; index < monthNames.Length; index++) {
        // NOTE: Month array is 13 entries long
        if (!string.IsNullOrEmpty(monthNames[index])) {
          Months.Add(new MonthInfo(Convert.ToInt16(index + 1), monthNames[index]));
        }
      }

      if (CustomerCreditCard.ExpMonth == 0) {
        // Figure out which month to select
        // Make it next month by default
        CustomerCreditCard.ExpMonth = Convert.ToInt16(DateTime.Now.Month + 1);
        CustomerCreditCard.ExpYear = Convert.ToInt16(DateTime.Now.Year);
        // If past December, then make it January of the next year
        if (CustomerCreditCard.ExpMonth > 12) {
          CustomerCreditCard.ExpMonth = 1;
          CustomerCreditCard.ExpYear += 1;
        }
      }
    }
    #endregion

    #region LoadYears Method
    public void LoadYears(int yearsInFuture = 20)
    {
      Years = new List<string>();
      for (int i = DateTime.Now.Year; i <= (DateTime.Now.Year + yearsInFuture); i++) {
        Years.Add(i.ToString());
      }
    }
    #endregion

    #region ProcessCreditCard Method
    public bool ProcessCreditCard(int custId)
    {
      // TODO: Make call to your CC processor here

      // Save Purchase Info
      SavePurchaseInformation(custId);

      // Simulate a long-running card process
      System.Threading.Thread.Sleep(5000);

      return true;
    }
    #endregion

    #region SavePurchaseInformation()
    protected void SavePurchaseInformation(int custId)
    {
      try {
        // Create Order Header
        OrderInformation = OrderHeaderRepository.Insert(new OrderHeader
        {
          CustomerId = custId,
          OrderDate = DateTime.Now,
          PromotionalCode = Cart.PromotionCode.PromotionalCode
        });
      }
      catch (Exception ex) {
        System.Diagnostics.Debug.WriteLine(ex.ToString());
      }

      try {
        // Create Order Details
        foreach (ShoppingCartItem item in Cart.Items) {
          OrderDetails.Add(OrderDetailRepository.Insert(new OrderDetail
          {
            OrderHeaderId = OrderInformation.OrderHeaderId,
            CustomerId = custId,
            Price = item.Price,
            ProductId = item.ProductId,
            ProductName = item.ProductName,
            Quantity = item.Quantity
          }));
        }
      }
      catch (Exception ex) {
        System.Diagnostics.Debug.WriteLine(ex.ToString());
      }

      try {
        // Store Credit Card Payment Info
        PaymentInformation = Repository.Insert(new CustomerPayment
        {
          AmountPaid = Cart.TotalCart,
          CCAuth = "AUTH-" + DateTime.Now.ToString().Replace("/", "-").Replace(" ", "-").Replace(":", "-").Replace("PM", "").Replace("AM", ""),
          CCType = CustomerCreditCard.CardType,
          CustomerId = custId,
          OrderHeaderId = OrderInformation.OrderHeaderId,
          Response = "Success"
        });
      }
      catch (Exception ex) {
        System.Diagnostics.Debug.WriteLine(ex.ToString());
      }
    }
    #endregion
  }
}
