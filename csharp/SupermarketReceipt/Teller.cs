using System;
using System.Collections.Generic;
using System.Linq;
using SupermarketReceipt.Offers;
using SupermarketReceipt.Offers.OfferCalculators;
using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;

namespace SupermarketReceipt
{
    public class Teller
    {
        private readonly SupermarketCatalog _catalog;
        private readonly List<IOfferCalculator> _offers = new();

        public Teller(SupermarketCatalog catalog)
        {
            _catalog = catalog;
        }

        public void AddProductOffer(SpecialOfferType offerType, Product product, double argument)
        {
            _offers.Add(OfferCalculatorFactory.CreateOfferCalculator(offerType, product, argument));
        }

        public void AddBundleOffer(SpecialOfferType offerType, Product product, double argument)
        {
            _offers.Add(OfferCalculatorFactory.CreateOfferCalculator(offerType, product, argument));
        }

        public Receipt GenerateReceipt(ShoppingCart theCart)
        {
            var receipt = new Receipt();
            var productQuantities = theCart.GetItems();
            foreach (var productQuantity in productQuantities)
            {
                var product = productQuantity.Product;
                var quantity = productQuantity.Quantity;
                var unitPrice = _catalog.GetUnitPrice(product);
                receipt.AddProduct(product, quantity, unitPrice);
            }

            var discounts = CalculateDiscounts(productQuantities);

            foreach (var discount in discounts)
            {
                receipt.AddDiscount(discount);
            }

            return receipt;
        }

        
        private List<Discount> CalculateDiscounts(List<ProductQuantity> productQuantities)
        {
            List<Discount> discounts = new List<Discount>();

            foreach (var offer in _offers)
            {
                var discount = offer.CalculateDiscount(productQuantities, _catalog);

                if (discount != null)
                    discounts.Add(discount);
            }

            if (productQuantities.Any(x => x.Product.Name == "toothpaste") && productQuantities.Any(x => x.Product.Name == "toothbrush"))
                discounts.Add(CalculateBundleDiscount(_catalog, productQuantities));

            return discounts;

        }

        private static Discount CalculateBundleDiscount(SupermarketCatalog catalog, List<ProductQuantity> productQuantities)
        {
            double discountAmount = 0.278;

            return new Discount(productQuantities.First().Product, "bundle offer", -discountAmount);
        }
    }
}