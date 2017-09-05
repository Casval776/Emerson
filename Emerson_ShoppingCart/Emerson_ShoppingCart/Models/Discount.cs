namespace Emerson_ShoppingCart.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public double OverridePrice { get; set; }
        public string DiscountCode { get; set; }
        public int ItemId { get; set; }
    }
}