using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaulsAutoParts.EntityLayer
{
  /// <summary>
  /// This class contains properties that map to each field in the dbo.OrderHeader table.
  /// </summary>

  [Table("OrderHeader", Schema = "dbo")]
  public partial class OrderHeader : AppEntityBase
  {
    public OrderHeader()
    {
    }

    /// <summary>
    /// Get/Set the OrderHeaderId value
    /// </summary>
    [Display(Name = "Order Header Id")]
    [Key]
    public int? OrderHeaderId { get; set; }


    /// <summary>
    /// Get/Set the CustomerId value
    /// </summary>
    [Display(Name = "Customer Id")]
    public int? CustomerId { get; set; }

    /// <summary>
    /// Get/Set the PromotionalCode value
    /// </summary>
    [Display(Name = "Promotional Code")]
    [StringLength(20, MinimumLength = 0, ErrorMessage = "Promotional Code must be between {2} and {1} characters long.")]
    public string PromotionalCode { get; set; }

    /// <summary>
    /// Get/Set the OrderDate value
    /// </summary>
    [Display(Name = "Order Date")]
    public DateTime? OrderDate { get; set; }

    #region ToString Override
    public override string ToString()
    {
      return $"{OrderHeaderId}";
    }
    #endregion
  }
}
