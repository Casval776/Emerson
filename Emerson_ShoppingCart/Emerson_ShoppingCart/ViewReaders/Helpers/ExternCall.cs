using System;

namespace Emerson_ShoppingCart.ViewReaders.Helpers
{
    public static class ExternCall
    {
        /// <summary>
        /// Gets the tax rate.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <returns>Random tax rate between 0-100% until I find an API that doesn't piss me off.</returns>
        public static double GetTaxRate(string region) =>
            new Random().NextDouble();
    }
}