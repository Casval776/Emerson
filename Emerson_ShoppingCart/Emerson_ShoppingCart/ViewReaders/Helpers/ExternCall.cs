using System;

namespace Emerson_ShoppingCart.ViewReaders.Helpers
{
    /// <summary>
    /// Intended Functionality was to be used for external call to acquire sales tax rate per region.
    /// HOWEVER, most public APIs are either way too verbose or require payment. This is just a workaround.
    /// </summary>
    public static class ExternCall
    {
        #region Public Static Members
        /// <summary>
        /// Gets the tax rate.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns>Random tax rate between 0-100% until I find an API that doesn't piss me off.</returns>
        public static double GetTaxRate(string region) =>
            new Random().NextDouble();
        #endregion
    }
}