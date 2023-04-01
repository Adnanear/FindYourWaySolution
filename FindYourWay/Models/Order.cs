using FindYourWay.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindYourWay.Models
{
    public class Order
    {
        public int Id { get; set; }

        [ForeignKey(nameof(Models.User))]
        public int BuyerId { get; set; }

        [ForeignKey(nameof(Models.Product))]
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get;set; }

        public DateTime? DeletedAt { get; set; }
    }
}
