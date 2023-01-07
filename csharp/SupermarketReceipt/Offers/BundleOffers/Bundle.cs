using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupermarketReceipt.Products;

namespace SupermarketReceipt.Offers.BundleOffers
{
    public class Bundle
    {
        public List<ProductQuantity> ProductsQuantities { get; }
        public Bundle(params ProductQuantity[] productQuantities)
        {
            ProductsQuantities = productQuantities.ToList();
        }
        public double CalculateBundlePrice(SupermarketCatalog catalog)
        {
            double bundlePrice = 0;

            foreach (var productQuantity in ProductsQuantities)
            {
                var productPrice = catalog.GetUnitPrice(productQuantity.Product);

                bundlePrice += productPrice * productQuantity.Quantity;
            }

            return bundlePrice;
        }
    }
}
