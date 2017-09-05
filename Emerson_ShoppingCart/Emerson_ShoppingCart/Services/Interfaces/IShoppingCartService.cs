using System.Collections.Generic;
using Emerson_ShoppingCart.Models;
using Emerson_ShoppingCart.Models.RequestModels;
using Emerson_ShoppingCart.ViewReaders;

namespace Emerson_ShoppingCart.Services.Interfaces
{
    /// <summary>
    /// Interface IShoppingCartService
    /// </summary>
    /// /// <seealso cref="ShoppingCartViewReader" />
    public interface IShoppingCartService
    {
        #region Interface Members
        /// <summary>
        /// Adds the selected items to the selected User's shopping cart
        /// </summary>
        /// <param name="grm">The GRM.</param>
        /// <returns><c>true</c> if Successful business transformations are performed, <c>false</c> otherwise.</returns>
        bool AddItems(GenericRequestModel grm);

        bool RemoveItems(GenericRequestModel grm);

        IEnumerable<Item> ListItems(GenericRequestModel grm);

        bool ClearCart(GenericRequestModel grm);

        double GetTotalCost(GenericRequestModel grm);

        double GetSalesTax(GenericRequestModel grm);

        double ApplyDiscountCode(DiscountRequestModel drm);

        double ApplyBuyOneGetOne(GenericRequestModel grm);
        #endregion
    }
}