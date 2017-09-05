using System.Collections.Generic;

namespace Emerson_ShoppingCart.Models.RequestModels
{
    public class GenericRequestModel
    {
        public IEnumerable<Item> Items { get; set; }
        public User CurrentUser { get; set; }
    }
}