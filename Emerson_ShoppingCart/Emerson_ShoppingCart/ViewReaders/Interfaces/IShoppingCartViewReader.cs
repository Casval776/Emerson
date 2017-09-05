using System.Collections.Generic;
using Emerson_ShoppingCart.Models;
using Emerson_ShoppingCart.Models.RequestModels;

namespace Emerson_ShoppingCart.ViewReaders.Interfaces
{
    /// <summary>
    /// Interface IShoppingCartViewReader
    /// </summary>
    /// /// <seealso cref="Emerson_ShoppingCart.ViewReaders.ShoppingCartViewReader" />
    public interface IShoppingCartViewReader
    {
        #region Interface Members
        /// <summary>
        /// Adds the given data to the ShoppingCart_LinkTable Table.
        /// </summary>
        /// <param name="grm">GenericRequestModel containing a User and a List of Items.</param>
        /// <returns><c>true</c> if successful DB Operation, <c>false</c> otherwise.</returns>
        bool AddItems(GenericRequestModel grm);

        /// <summary>
        /// Removes the given data from the ShoppingCart_LinkTable Table.
        /// </summary>
        /// <param name="grm">GenericRequestModel containing a User and a List of Items.</param>
        /// <returns><c>true</c> if successful DB Operation, <c>false</c> otherwise.</returns>
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