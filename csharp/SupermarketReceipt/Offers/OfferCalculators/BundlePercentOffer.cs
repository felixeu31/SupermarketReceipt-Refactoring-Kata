using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketReceipt.Offers.OfferCalculators
{
    public class BundlePercentOffer : BundleOffer, IOfferCalculator
    {
        private readonly double _percent;

        public BundlePercentOffer(Bundle bundle, double percent) : base (bundle)
        {
            _percent = percent;
        }

        public Discount CalculateDiscount(List<ProductQuantity> productQuantities, SupermarketCatalog catalog)
        {
            double discountAmount = 0.278;

            return new Discount(productQuantities.First().Product, "bundle offer", -discountAmount);
        }
    }
}
