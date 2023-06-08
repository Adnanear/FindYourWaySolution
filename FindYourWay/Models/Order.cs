using FindYourWay.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FindYourWay.Models
{
    public class Order
    {
        public int Id { get; set; }

        // We declare the field as foreign key to create association with `Client`
        [ForeignKey(nameof(Models.Client))]
        public int ClientId { get; set; }

        // We declare the field as foreign key to create association with `Service`
        [ForeignKey(nameof(Models.Service))]
        public int ServiceId { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get;set; }

        public DateTime? DeletedAt { get; set; }
    }
}
