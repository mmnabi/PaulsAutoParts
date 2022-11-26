using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaulsAutoParts.EntityLayer
{
  /// <summary>
  /// This class contains properties that map to each field in the dbo.OrderDetail table.
  /// </summary>

  [Table("OrderDetail", Schema = "dbo")]
  public partial class OrderDetail : AppEntityBase
  {
    public OrderDetail()
    {
    }

    /// <summary>
    /// Get/Set the OrderDetailId value
    /// </summary>
    [Display(Name = "Order Detail Id")]
    [Key]
    public int? OrderDetailId { get; set; }

    /// <summary>
    /// Get/Set the CustomerId value
    /// </summary>
    [Display(Name = "Customer Id")]
    public int? CustomerId { get; set; }

    /// <summary>
    /// Get/Set the OrderHeaderId value
    /// </summary>
    [Display(Name = "Order Header Id")]
    public int? OrderHeaderId { get; set; }

    /// <summary>
    /// Get/Set the ProductId value
    /// </summary>
    [Display(Name = "Product Id")]
    public int? ProductId { get; set; }

    /// <summary>
    /// Get/Set the Quantity value
    /// </summary>
    [Display(Name = "Quantity")]
    public int? Quantity { get; set; }

    /// <summary>
    /// Get/Set the ProductName value
    /// </summary>
    [Display(Name = "Product Name")]
    [Required(ErrorMessage = "Product Name must be filled in.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "Product Name must be between {2} and {1} characters long.")]
    public string ProductName { get; set; }

    /// <summary>
    /// Get/Set the Price value
    /// </summary>
    [Display(Name = "Price")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? Price { get; set; }

    #region ToString Override
    public override string ToString()
    {
      return $"{ProductName}";
    }
    #endregion
  }
}
