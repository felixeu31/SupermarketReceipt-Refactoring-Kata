using System.Collections.Generic;

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

            OfferCalculator.HandleOffers(receipt, _offers, _catalog, productQuantities);

            return receipt;
        }
    }
}