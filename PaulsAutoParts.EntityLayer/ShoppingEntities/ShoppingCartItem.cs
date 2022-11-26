using System;
using System.ComponentModel.DataAnnotations;

namespace PaulsAutoParts.EntityLayer
{
  /// <summary>
  /// This class holds a single item in the shopping cart
  /// </summary>
  public partial class ShoppingCartItem
  {
    public ShoppingCartItem()
    {
      DateAdded = DateTime.Now;
      Quantity = 1;
    }

    /// <summary>
    /// Get/Set the CustomerId value
    /// </summary>
    [Display(Name = "Customer Id")]
    public int CustomerId { get; set; }

    /// <summary>
    /// Get/Set the ProductId value
    /// </summary>
    [Display(Name = "Product Id")]
    public int ProductId { get; set; }

    /// <summary>
    /// Get/Set the ProductName value
    /// </summary>
    [Display(Name = "Product Name")]
    public string ProductName { get; set; }

    /// <summary>
    /// Get/Set the Price value
    /// </summary>
    [Display(Name = "Price")]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }

    /// <summary>
    /// Get/Set the Total Price (Quantity * Price)
    /// </summary>
    [Display(Name = "Total Price")]
    [DataType(DataType.Currency)]
    public decimal TotalPrice
    {
      get {
        return Quantity * Price;
      }
    }

    /// <summary>
    /// Get/Set the quantity
    /// </summary>
    [Display(Name = "Quantity")]
    public int Quantity { get; set; }

    /// <summary>
    /// Get/Set the DateAdded value
    /// </summary>
    [Display(Name = "Date Added")]
    public DateTime DateAdded { get; set; }

    #region ToString Override
    public override string ToString()
    {
      return $"{ProductName}";
    }
    #endregion
  }
}
