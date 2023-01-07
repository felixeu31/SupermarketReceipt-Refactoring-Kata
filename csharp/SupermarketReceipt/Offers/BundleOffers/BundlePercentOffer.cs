using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketReceipt.Offers.BundleOffers
{
    public class BundlePercentOffer : BundleOffer, IOffer
    {
        private readonly double _percent;

        public BundlePercentOffer(Bundle bundle, double percent) : base(bundle)
        {
            _percent = percent;
        }

        public Discount CalculateDiscount(List<ProductQuantity> productQuantities, SupermarketCatalog catalog)
        {
            var bundlePrice = Bundle.CalculateBundlePrice(catalog);

            var completedBundles = CalculateCompletedBundles(productQuantities);

            double discountAmount = bundlePrice * _percent / 100 * completedBundles;

            return new Discount(productQuantities.First().Product, "bundle offer", -discountAmount);
        }

        private int CalculateCompletedBundles(List<ProductQuantity> productQuantities)
        {
            int? completedBundles = null;

            foreach (var bundleProductQuantity in Bundle.ProductsQuantities)
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
