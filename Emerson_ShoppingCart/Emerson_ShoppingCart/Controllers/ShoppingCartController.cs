using System;
using System.Collections.Generic;
using System.Web.Http;
using Emerson_ShoppingCart.Controllers.Interfaces;
using Emerson_ShoppingCart.Models;
using Emerson_ShoppingCart.Models.RequestModels;
using Emerson_ShoppingCart.Services.Interfaces;

namespace Emerson_ShoppingCart.Controllers
{
    public class ShoppingCartController : ApiController, IShoppingCartController
    {
        private readonly IShoppingCartService _svc;

        public ShoppingCartController(IShoppingCartService svc)
        {
            _svc = svc;
        }

        [HttpPost]
        [Route("api/shopcart/additems")]
        public IHttpActionResult AddItems([FromBody] GenericRequestModel grm)
        {
            var result = _svc.AddItems(grm);
            return Ok(new
            {
                result
            });
        }

        [HttpPost]
        [Route("api/shopcart/removeitems")]
        public IHttpActionResult RemoveItems([FromBody] GenericRequestModel grm)
        {
            var result = _svc.RemoveItems(grm);
            return Ok(new
            {
                result
            });
        }

        [HttpGet]
        [Route("api/shopcart/listitems")]
        public IHttpActionResult ListItems([FromBody] GenericRequestModel grm)
        {
            var result = _svc.ListItems(grm);
            return Ok(new
            {
                result
            });
        }

        [HttpPost]
        [Route("api/shopcart/clear")]
        public IHttpActionResult ClearCart([FromBody] GenericRequestModel grm)
        {
            var result = _svc.ClearCart(grm);
            return Ok(new
            {
                result
            });
        }

        [HttpGet]
        [Route("api/shopcart/gettotal")]
        public IHttpActionResult GetTotalCost([FromBody] GenericRequestModel grm)
        {
            var result = _svc.GetTotalCost(grm);
            return Ok(new
            {
                result
            });
        }

        [HttpGet]
        [Route("api/shopcart/gettax")]
        public IHttpActionResult GetSalesTax([FromBody] GenericRequestModel grm)
        {
            var result = _svc.GetSalesTax(grm);
            return Ok(new
            {
                result
            });
        }

        [HttpGet]
        [Route("api/shopcart/discountcode")]
        public IHttpActionResult ApplyDiscountCode([FromBody] DiscountRequestModel drm)
        {
            var result = _svc.ApplyDiscountCode(drm);
            return Ok(new
            {
                result
            });
        }

        [HttpGet]
        [Route("api/shopcart/applybogo")]
        public IHttpActionResult ApplyBuyOneGetOne(GenericRequestModel grm)
        {
            var result = _svc.ApplyBuyOneGetOne(grm);
            return Ok(new
            {
                result
            });
        }
    }
}