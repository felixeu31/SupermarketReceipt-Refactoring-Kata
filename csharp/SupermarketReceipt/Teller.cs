using System;
using System.Collections.Generic;
using System.Linq;
using SupermarketReceipt.Offers;
using SupermarketReceipt.Offers.BundleOffers;
using SupermarketReceipt.Offers.OfferCalculators;
using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;

namespace SupermarketReceipt
{
    public class Teller
    {
        private readonly SupermarketCatalog _catalog;
        private readonly List<IOffer> _offers = new();

        public Teller(SupermarketCatalog catalog)
        {
            _catalog = catalog;
        }

        public void AddProductOffer(OfferType offerType, Product product, double argument)
        {
            _offers.Add(ProductOfferFactory.CreateOfferCalculator(offerType, product, argument));
        }

        public void AddBundleOffer(OfferType offerType, Bundle bundle, double argument)
        {
            _offers.Add(new BundlePercentOffer(bundle, argument));
        }
        
        public Receipt GenerateReceipt(ShoppingCart theCart)
        {
            var receipt = new Receipt();
            var productQuantities = theCart.GetItems();

            foreach (var productQuantity in productQuantities)
            {
                var unitPrice = _catalog.GetUnitPrice(productQuantity.Product);
                receipt.AddProduct(productQuantity.Product, productQuantity.Quantity, unitPrice);
            }

            foreach (var offer in _offers)
            {
                var discount = offer.CalculateDiscount(productQuantities, _catalog);

                if (discount != null)
                    receipt.AddDiscount(discount);
            }

            return receipt;
        }
    }
}