namespace Emerson_ShoppingCart.Models.RequestModels
{
    public class DiscountRequestModel
    {
        public User CurrentUser { get; set; }
        public string DiscountCode { get; set; }
    }
}