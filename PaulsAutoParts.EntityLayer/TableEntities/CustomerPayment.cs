using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaulsAutoParts.EntityLayer
{
  /// <summary>
  /// This class contains properties that map to each field in the dbo.CustomerPayment table.
  /// </summary>

  [Table("CustomerPayment", Schema = "dbo")]
  public partial class CustomerPayment : AppEntityBase
  {
    public CustomerPayment()
    {
    }

    /// <summary>
    /// Get/Set the CustomerPaymentId value
    /// </summary>
    [Display(Name = "Customer Payment Id")]
    [Key]
    public int? CustomerPaymentId { get; set; }


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
    /// Get/Set the CCType value
    /// </summary>
    [Display(Name = "CC Type")]
    [StringLength(20, MinimumLength = 0, ErrorMessage = "CC Type must be between {2} and {1} characters long.")]
    public string CCType { get; set; }

    /// <summary>
    /// Get/Set the CCAuth value
    /// </summary>
    [Display(Name = "CC Auth")]
    [Required(ErrorMessage = "CC Auth must be filled in.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "CC Auth must be between {2} and {1} characters long.")]
    public string CCAuth { get; set; }

    /// <summary>
    /// Get/Set the Response value
    /// </summary>
    [Display(Name = "Response")]
    [Required(ErrorMessage = "Response must be filled in.")]
    [StringLength(1073741823, MinimumLength = 1, ErrorMessage = "Response must be between {2} and {1} characters long.")]
    public string Response { get; set; }

    /// <summary>
    /// Get/Set the AmountPaid value
    /// </summary>
    [Display(Name = "Amount Paid")]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18,2)")]
    public decimal? AmountPaid { get; set; }

    #region ToString Override
    public override string ToString()
    {
      return $"{CCType}";
    }
    #endregion
  }
}
