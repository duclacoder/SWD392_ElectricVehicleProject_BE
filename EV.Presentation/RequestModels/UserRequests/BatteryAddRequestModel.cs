using System.ComponentModel.DataAnnotations;

namespace EV.Presentation.RequestModels.UserRequests
{
    public class BatteryAddRequestModel
    {
        [Required]
        public int? UserId { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string? BatteryName { get; set; }

        [Required]
        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [StringLength(50)]
        public string? Brand { get; set; }

        // Capacity in kWh (or Ah depending on your system)
        [Required]
        [Range(1, 1000, ErrorMessage = "Capacity must be between 1 and 1000.")]
        public int? Capacity { get; set; }

        // Voltage in Volts
        [Required]
        [Range(1, 999, ErrorMessage = "Voltage must be between 1V and 999V.")]
        public decimal? Voltage { get; set; }

        [Required]
        [Range(0, 240, ErrorMessage = "Warranty must be between 0 and 240 months.")]
        public int? WarrantyMonths { get; set; }

        [Required]
        [Range(1, 100000000, ErrorMessage = "Price must be between 1 and 100,000,000.")]
        public decimal? Price { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z]{3}$", ErrorMessage = "Currency must be a 3-letter ISO code (e.g., USD, EUR).")]
        public string? Currency { get; set; }
    }
}
