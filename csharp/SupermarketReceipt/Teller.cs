using System.Collections.Generic;
using SupermarketReceipt.Offers;
using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;

namespace SupermarketReceipt
{
    public class Teller
    {
        private readonly SupermarketCatalog _catalog;
        private readonly Dictionary<Product, Offer> _offers = new Dictionary<Product, Offer>();

        public Teller(SupermarketCatalog catalog)
        {
            _catalog = catalog;
        }

        public void AddSpecialOffer(SpecialOfferType offerType, Product product, double argument)
        {
            _offers[product] = new Offer(offerType, product, argument);
        }

        public Receipt GenerateReceipt(ShoppingCart theCart)
        {
            var receipt = new Receipt();
            var productQuantities = theCart.GetItems();
            foreach (var productQuantity in productQuantities)
            {
                var product = productQuantity.Key;
                var quantity = productQuantity.Value;
                var unitPrice = _catalog.GetUnitPrice(product);
                receipt.AddProduct(product, quantity, unitPrice);
            }

            var discounts = OfferTeller.CalculateDiscounts(_offers, _catalog, productQuantities);

            foreach (var discount in discounts)
            {
                receipt.AddDiscount(discount);
            }

            return receipt;
        }
    }
}