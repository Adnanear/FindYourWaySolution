using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindYourWay.Models.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }

        [Required]
        public int BuyerId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [Range(1, 100, ErrorMessage = "Quantity must be between 1 to 100.")]
        public int Quantity { get; set; }

        [Required]
        [Range(0, 3)]
        public int OrderStatus { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
