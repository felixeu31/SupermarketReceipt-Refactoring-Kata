using System.Collections.Generic;
using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;

namespace SupermarketReceipt.Offers;

public class OfferTeller
{
    public static List<Discount> CalculateDiscounts(Dictionary<Product, Offer> offers, SupermarketCatalog catalog, Dictionary<Product, double> productQuantities)
    {
        List<Discount> discounts = new List<Discount>();

        foreach (var product in productQuantities.Keys)
        {
            if (!offers.ContainsKey(product))
            {
                continue;
            }

            var quantity = (int)productQuantities[product];
            var offer = offers[product];
            var unitPrice = catalog.GetUnitPrice(product);

            var offerCalculator = OfferCalculatorFactory.CreateOfferCalculator(offer);

            var discount = offerCalculator.CalculateDiscount(quantity, unitPrice);

            if (discount != null)
                discounts.Add(discount);
        }

        return discounts;
    }
}