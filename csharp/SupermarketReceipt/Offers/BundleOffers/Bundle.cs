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

        public int CalculateCompletedBundles(List<ProductQuantity> productQuantities)
        {
            int? completedBundles = null;

            foreach (var bundleProductQuantity in ProductsQuantities)
            {
                var existingProductQuantity = productQuantities.FirstOrDefault(x => Equals(x.Product, bundleProductQuantity.Product));
                
                if (existingProductQuantity == null) { return 0; }

                var productChunks = Convert.ToInt32(existingProductQuantity.Quantity / bundleProductQuantity.Quantity);

                if (completedBundles == null || productChunks < completedBundles)
                    completedBundles = productChunks;
            }

            return completedBundles.GetValueOrDefault();
        }
    }
}
