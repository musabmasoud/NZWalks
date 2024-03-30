using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO
{
    public class UpdateRegionRequestDTO
    {
        [Required]
        [MinLength(3, ErrorMessage = "the minimus code has been 3 charector")]
        [MaxLength(3, ErrorMessage = "the maximus code has been 3 charector")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "the maximus name has been 3 charector")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
