using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketReceipt.Offers.OfferCalculators
{
    public class PercentOffer : ProductOffer, IOfferCalculator
    {
        private readonly double _percent;

        public PercentOffer(Product product, double percent) : base (product)
        {
            _percent = percent;
        }

        public Discount CalculateDiscount(List<ProductQuantity> productQuantities, SupermarketCatalog catalog)
        {
            var productQuantity = productQuantities.FirstOrDefault(x => Equals(x.Product, _product));

            if (productQuantity == null)
            {
                return null;
            }

            var quantity = (int)productQuantity.Quantity;

            var unitPrice = catalog.GetUnitPrice(_product);
            
            if (quantity == 0)
                return null;

            return new Discount(_product, _percent + "% off", -quantity * unitPrice * _percent / 100.0);
        }
    }
}
