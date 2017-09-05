using System.Collections.Generic;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Emerson_ShoppingCart.Controllers.Interfaces;
using Emerson_ShoppingCart.Database;
using Emerson_ShoppingCart.Models;
using Emerson_ShoppingCart.Models.RequestModels;
using Emerson_ShoppingCart.Services.Interfaces;

namespace Emerson_ShoppingCart.Tests
{
    /// <summary>
    /// NOTE: Probably need to implement the repository pattern to enable proper unit-testing.
    /// Too many (more than none) work-arounds were made for the SQLite DB Initialization problem.
    /// </summary>
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AddItems_ControllerTest()
        {
            //Arrange
            Bootstrapper.Run();
            DatabaseDriver.ValidateDBStatus();
            var controller = Bootstrapper._container.Resolve<IShoppingCartController>();
            var service = Bootstrapper._container.Resolve<IShoppingCartService>();
            var itemList = new List<Item>
            {
                new Item
                {
                    Id = 6,
                    Name = "Test Item",
                    Description = "Test Description",
                    Price = 9999
                }
            };
            var user = new User
            {
                Id = 1
            };
            var grm = new GenericRequestModel
            {
                CurrentUser = user,
                Items = itemList
            };

            //Act
            //var result = controller.AddItems(grm);
            var result = service.AddItems(grm);

            //Assert
            Assert.AreEqual(result, true);
        }
    }
}
