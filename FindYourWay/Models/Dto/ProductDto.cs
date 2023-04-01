using System.ComponentModel.DataAnnotations;

namespace FindYourWay.Models.Dto
{
    public class ProductDto
    {

        public int Id { get; set; }

        [StringLength(maximumLength: 125, ErrorMessage = "Name must not exceed 125 charcters long.")]
        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        [Range(0, 1000000, ErrorMessage = "Price must be between 0 to 1000000.")]
        public float Price { get; set; }

        public bool Available { get; set; }

        [Url(ErrorMessage = "Product image must be a valid url.")]
        public string? Image { get; set; }
    }
}
