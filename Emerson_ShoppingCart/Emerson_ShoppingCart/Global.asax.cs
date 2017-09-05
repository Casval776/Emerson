using System.Web.Http;
using Emerson_ShoppingCart.Database;

namespace Emerson_ShoppingCart
{
    /// <summary>
    /// Class WebApiApplication.
    /// </summary>
    /// <seealso cref="System.Web.HttpApplication" />
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// ASP.Net Native Code
        /// </summary>
        protected void Application_Start()
        {
            //Start the dependency injection
            Bootstrapper.Run();

            //Validate DB Status
            DatabaseDriver.ValidateDBStatus();

            //Native ASP.Net Code
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
