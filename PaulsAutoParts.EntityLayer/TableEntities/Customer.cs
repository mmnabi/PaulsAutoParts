using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaulsAutoParts.EntityLayer
{
  /// <summary>
  /// This class contains properties that map to each field in the dbo.Customer table.
  /// </summary>

  [Table("Customer", Schema = "dbo")]
  public partial class Customer : AppEntityBase
  {
    public Customer()
    {
    }

    /// <summary>
    /// Get/Set the CustomerId value
    /// </summary>
    [Display(Name = "Customer Id")]
    [Key]
    public int? CustomerId { get; set; }

    /// <summary>
    /// Get/Set the FirstName value
    /// </summary>
    [Display(Name = "First Name")]
    [Required(ErrorMessage = "First Name must be filled in.")]
    [StringLength(50, MinimumLength = 1, ErrorMessage = "First Name must be between {2} and {1} characters long.")]
    public string FirstName { get; set; }

    /// <summary>
    /// Get/Set the LastName value
    /// </summary>
    [Display(Name = "Last Name")]
    [Required(ErrorMessage = "Last Name must be filled in.")]
    [StringLength(75, MinimumLength = 1, ErrorMessage = "Last Name must be between {2} and {1} characters long.")]
    public string LastName { get; set; }

    /// <summary>
    /// Get/Set the EmailAddress value
    /// </summary>
    [Display(Name = "Email Address")]
    [Required(ErrorMessage = "Email Address must be filled in.")]
    [StringLength(255, MinimumLength = 1, ErrorMessage = "Email Address must be between {2} and {1} characters long.")]
    public string EmailAddress { get; set; }

    /// <summary>
    /// Get/Set the Phone value
    /// </summary>
    [Display(Name = "Phone")]
    [StringLength(50, MinimumLength = 0, ErrorMessage = "Phone must be between {2} and {1} characters long.")]
    public string Phone { get; set; }

    #region ToString Override
    public override string ToString()
    {
      return $"{FirstName}";
    }
    #endregion
  }
}
