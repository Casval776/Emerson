using System.Collections;
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
        public bool AddItems(GenericRequestModel grm)
        {
            return _vreader.AddItems(grm);
        }

        public bool RemoveItems(GenericRequestModel grm)
        {
            return _vreader.RemoveItems(grm);
        }

        public IEnumerable<Item> ListItems(GenericRequestModel grm)
        {
            return _vreader.ListItems(grm);
        }

        public bool ClearCart(GenericRequestModel grm)
        {
            return _vreader.ClearCart(grm);
        }

        public double GetTotalCost(GenericRequestModel grm) =>
            _vreader.GetTotalCost(grm);

        public double GetSalesTax(GenericRequestModel grm) =>
            _vreader.GetSalesTax(grm);

        public double ApplyDiscountCode(DiscountRequestModel drm) =>
            _vreader.ApplyDiscountCode(drm);

        public double ApplyBuyOneGetOne(GenericRequestModel grm) =>
            _vreader.ApplyBuyOneGetOne(grm);

        #endregion
    }
}