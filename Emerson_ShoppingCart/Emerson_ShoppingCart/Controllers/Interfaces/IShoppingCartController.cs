using System.Web.Http;
using Emerson_ShoppingCart.Models.RequestModels;

namespace Emerson_ShoppingCart.Controllers.Interfaces
{
    /// <summary>
    /// Interface IShoppingCartController
    /// </summary>
    public interface IShoppingCartController
    {
        /// <summary>
        /// Adds the items.
        /// </summary>
        /// <param name="grm">The GRM.</param>
        /// <returns>IHttpActionResult.</returns>
        IHttpActionResult AddItems(GenericRequestModel grm);

        IHttpActionResult RemoveItems(GenericRequestModel grm);

        IHttpActionResult ListItems(GenericRequestModel grm);

        IHttpActionResult ClearCart(GenericRequestModel grm);

        IHttpActionResult GetTotalCost(GenericRequestModel grm);

        IHttpActionResult GetSalesTax(GenericRequestModel grm);

        IHttpActionResult ApplyDiscountCode(DiscountRequestModel drm);

        IHttpActionResult ApplyBuyOneGetOne(GenericRequestModel grm);

        //IHttpActionResult ApplyRegionalSalesTax();
    }
}