using System.ComponentModel.DataAnnotations;

namespace MyFirstApi.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code has to have minimum 3 chcar")]
        [MaxLength(3, ErrorMessage = "Code has to have max 3 chcar")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Code has to have max 100 chcar")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }
    }
}
