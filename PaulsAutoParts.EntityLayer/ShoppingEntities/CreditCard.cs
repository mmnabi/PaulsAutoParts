using System.ComponentModel.DataAnnotations;

namespace PaulsAutoParts.EntityLayer
{
  public partial class CreditCard
  {
    [Required]
    [StringLength(20)]
    [Display(Name = "Select Card Type")]
    public string CardType { get; set; }

    [Required]
    [StringLength(100)]
    [Display(Name = "Name on Card")]
    public string NameOnCard { get; set; }

    [Required]
    [StringLength(25)]
    [Display(Name = "Credit Card Number")]
    public string CardNumber { get; set; }

    [Required]
    [StringLength(4)]
    [Display(Name = "Code on Back")]
    public string SecurityCode { get; set; }

    [Display(Name = "Expiration Month")]
    public int ExpMonth { get; set; }

    [Display(Name = "Expiration Year")]
    public int ExpYear { get; set; }

    [Required]
    [StringLength(10)]
    [Display(Name = "Billing Postal Code")]
    public string BillingPostalCode { get; set; }
  }
}
