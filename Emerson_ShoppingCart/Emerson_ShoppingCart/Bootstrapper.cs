using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Emerson_ShoppingCart.Controllers;
using Emerson_ShoppingCart.Controllers.Interfaces;
using Emerson_ShoppingCart.Services;
using Emerson_ShoppingCart.Services.Interfaces;
using Emerson_ShoppingCart.ViewReaders;
using Emerson_ShoppingCart.ViewReaders.Interfaces;

namespace Emerson_ShoppingCart
{
    /// <summary>
    /// Class used to encapsulate AutoFac DI Functionality.
    /// Called on application start.
    /// </summary>
    public static class Bootstrapper
    {
        #region Public Static Members
        /// <summary>
        /// Publicly accessible container to resolve types.
        /// This was primarily used because of unit testing.
        /// </summary>
        public static IContainer _container;
        #endregion

        #region Public Static Methods
        /// <summary>
        /// Handles AutoFac DI and Type Registration
        /// </summary>
        public static void Run()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Register your Web API Services.
            builder.RegisterType<ShoppingCartController>().As<IShoppingCartController>().SingleInstance();
            builder.RegisterType<ShoppingCartService>().As<IShoppingCartService>().SingleInstance();
            builder.RegisterType<ShoppingCartViewReader>().As<IShoppingCartViewReader>().SingleInstance();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            //Register the container as a publicly accessible variable
            _container = container;
        }
        #endregion
    }
}