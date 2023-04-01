namespace FindYourWay.Models
{
    public class Order
    {
        public int Id { get; set; }

        public User? Buyer { get; set; }

        public Product? Product { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get;set; }

        public DateTime? DeletedAt { get; set; }
    }
}
