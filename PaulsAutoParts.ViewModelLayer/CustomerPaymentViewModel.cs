using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PaulsAutoParts.Common;
using PaulsAutoParts.EntityLayer;

namespace PaulsAutoParts.ViewModelLayer
{
  public class CustomerPaymentViewModel : AppViewModelBase
  {
    #region Constructors
    /// <summary>
    ///  NOTE: You need to have a parameterless constructor for Post-Backs in MVC    
    /// </summary>
    public CustomerPaymentViewModel() : base()
    {
      Init();
    }

    public CustomerPaymentViewModel(IRepository<CustomerPayment, CustomerPaymentSearch> repository) : base()
    {
      Init();

      Repository = repository;
    }

    public CustomerPaymentViewModel(IRepository<CustomerPayment, CustomerPaymentSearch> repository,
      IRepository<OrderHeader, OrderHeaderSearch> ohRepository,
      IRepository<OrderDetail, OrderDetailSearch> odRepository) : base()
    {
      Init();

      Repository = repository;
      OrderHeaderRepository = ohRepository;
      OrderDetailRepository = odRepository;
    }
    #endregion

    #region Properties
    public IRepository<CustomerPayment, CustomerPaymentSearch> Repository { get; set; }
    public IRepository<OrderHeader, OrderHeaderSearch> OrderHeaderRepository { get; set; }
    public IRepository<OrderDetail, OrderDetailSearch> OrderDetailRepository { get; set; }

    public List<CustomerPayment> DataCollection { get; set; }
    public CustomerPayment SelectedEntity { get; set; }
    public CustomerPaymentSearch SearchEntity { get; set; }

    public OrderHeader OrderInformation { get; set; }
    public List<OrderDetail> OrderDetails { get; set; }

    public bool IsFromCustomerMaintenance { get; set; }

    [DataType(DataType.Currency)]
    public decimal TotalSpent { get; set; }
    #endregion

    #region Init Method
    public override void Init()
    {
      base.Init();

      DataCollection = new List<CustomerPayment>();
      SelectedEntity = new CustomerPayment();
      SearchEntity = new CustomerPaymentSearch();

      OrderInformation = new OrderHeader();
      OrderDetails = new List<OrderDetail>();
    }
    #endregion

    #region Get Method
    public override void Get()
    {
      IsDetailVisible = false;

      if (Repository == null) {
        throw new ApplicationException("Must set the Repository property.");
      }
      else {
        DataCollection = Repository.Get();
      }

      if (DataCollection != null) {
        TotalRecords = DataCollection.Count;

        // Add up Total
        AddUpTotal();
      }
    }
    #endregion

    #region Get(id) Method
    public override void Get(int id)
    {
      IsDetailVisible = true;

      if (Repository == null) {
        throw new ApplicationException("Must set the Repository property.");
      }
      else {
        SelectedEntity = Repository.Get(id);
      }

      if (SelectedEntity != null) {
        TotalRecords = 1;
      }
    }
    #endregion

    #region GetOrderInformation Method
    public void GetOrderInformation(int id)
    {
      IsDetailVisible = false;

      if (OrderHeaderRepository == null) {
        throw new ApplicationException("Must set the OrderHeaderRepository property.");
      }
      else if (OrderDetailRepository == null) {
        throw new ApplicationException("Must set the OrderDetailRepository property.");
      }
      else {
        // Get Order Header Information
        OrderInformation = OrderHeaderRepository.Get(id);
        // Setup PromoCode for display on web page
        OrderInformation.PromotionalCode = string.IsNullOrEmpty(OrderInformation.PromotionalCode) ? "n/a" : OrderInformation.PromotionalCode;
        // Get Order Details
        OrderDetails = OrderDetailRepository.Search(new OrderDetailSearch
        {
          OrderHeaderId = id
        });
      }

      TotalRecords = OrderDetails.Count;
    }
    #endregion

    #region Search Method
    public override void Search()
    {
      IsDetailVisible = false;

      if (Repository == null) {
        throw new ApplicationException("Must set the Repository property.");
      }
      else {
        DataCollection = Repository.Search(SearchEntity).OrderBy(p => p.CCType).ToList();
      }

      if (DataCollection != null) {
        TotalRecords = DataCollection.Count;

        // Add up Total
        AddUpTotal();
      }
    }
    #endregion

    #region AddUpTotal Method
    protected void AddUpTotal()
    {
      TotalSpent = DataCollection.Sum(p => p.AmountPaid.Value);
    }
    #endregion

    #region CreateEmptyEntity Method
    public override void CreateEmptyEntity()
    {
      SelectedEntity = Repository.CreateEmpty();
    }
    #endregion

    #region Save Method
    public override bool Save()
    {
      bool ret = false;

      if (Validate()) {
        if (SelectedEntity.CustomerPaymentId.HasValue) {
          // Update the current entity
          Repository.Update(SelectedEntity);
        }
        else {
          // Add a new entity
          Repository.Insert(SelectedEntity);
        }
        ret = true;
      }

      return ret;
    }
    #endregion

    #region Validate Method
    public override bool Validate()
    {
      IsValid = false;
      Messages = new List<string>();

      // TODO: Validate Your Properties Here


      IsValid = (Messages.Count == 0);

      return IsValid;
    }
    #endregion

    #region Delete Method
    public override bool Delete(int id)
    {
      // Delete the entity by id
      Repository.Delete(id);

      return true;
    }
    #endregion
  }
}
