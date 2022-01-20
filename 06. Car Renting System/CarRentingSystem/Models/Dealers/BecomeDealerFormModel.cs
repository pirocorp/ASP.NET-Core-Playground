namespace CarRentingSystem.Models.Dealers
{
    using System.ComponentModel.DataAnnotations;

    using static Data.DataConstants.Dealer;

    public class BecomeDealerFormModel
    {
        [Required]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
        public string? Name { get; set; }

        [Display(Name = "Phone Number")]
        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string? PhoneNumber { get; set; }
    }
}
