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
        private readonly List<Offer> _offers = new();

        public Teller(SupermarketCatalog catalog)
        {
            _catalog = catalog;
        }

        public void AddSpecialOffer(SpecialOfferType offerType, Product product, double argument)
        {
            _offers.Add(new Offer(offerType, product, argument));
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

        // How can I test this class. It has a collaborator instantiated by a factory.
        // Do i want to test this class without the collaborator taking place.
        // Does it mean I have a bad design for this class??
        // Should i use it as a pitch point (Working effectively with legacy code)
        public List<Discount> CalculateDiscounts(List<ProductQuantity> productQuantities)
        {
            List<Discount> discounts = new List<Discount>();

            foreach (var offer in _offers)
            {
                var productQuantity = productQuantities.FirstOrDefault(x => Equals(x.Product, offer.Product));

                if (productQuantity == null)
                {
                    continue;
                }

                var quantity = (int)productQuantity.Quantity;
                var unitPrice = _catalog.GetUnitPrice(offer.Product);

                var offerCalculator = OfferCalculatorFactory.CreateOfferCalculator(offer);

                var discount = offerCalculator.CalculateDiscount(quantity, unitPrice);

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