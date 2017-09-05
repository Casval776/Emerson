using System.Collections.Generic;
using Emerson_ShoppingCart.Models;
using Emerson_ShoppingCart.Models.RequestModels;
using Emerson_ShoppingCart.Services.Interfaces;
using Emerson_ShoppingCart.ViewReaders.Interfaces;

namespace Emerson_ShoppingCart.Services
{
    /// <summary>
    /// Service class for the ShoppingCart Controller.
    /// Handles Business Logic that may manipulate data before interaction with DB.
    /// </summary>
    /// <seealso cref="Emerson_ShoppingCart.Services.Interfaces.IShoppingCartService" />
    public class ShoppingCartService : IShoppingCartService
    {
        #region Private Members
        private readonly IShoppingCartViewReader _vreader;
        #endregion

        #region Constructors        
        /// <summary>
        /// Initializes a new instance of the <see cref="ShoppingCartService"/> class.
        /// </summary>
        /// <param name="vreader">The vreader.</param>
        public ShoppingCartService(IShoppingCartViewReader vreader)
        {
            _vreader = vreader;
        }
        #endregion

        #region Interface Members
        /// <inheritdoc />
        public bool AddItems(GenericRequestModel grm) =>
            _vreader.AddItems(grm);

        /// <inheritdoc />
        public bool RemoveItems(GenericRequestModel grm) => 
            _vreader.RemoveItems(grm);

        /// <inheritdoc />
        public IEnumerable<Item> ListItems(GenericRequestModel grm) =>
            _vreader.ListItems(grm);

        /// <inheritdoc />
        public bool ClearCart(GenericRequestModel grm) =>
            _vreader.ClearCart(grm);

        /// <inheritdoc />
        public double GetTotalCost(GenericRequestModel grm) =>
            _vreader.GetTotalCost(grm);

        /// <inheritdoc />
        public double GetSalesTax(GenericRequestModel grm) =>
            _vreader.GetSalesTax(grm);

        /// <inheritdoc />
        public double ApplyDiscountCode(DiscountRequestModel drm) =>
            _vreader.ApplyDiscountCode(drm);

        /// <inheritdoc />
        public double ApplyBuyOneGetOne(GenericRequestModel grm) =>
            _vreader.ApplyBuyOneGetOne(grm);
        #endregion
    }
}