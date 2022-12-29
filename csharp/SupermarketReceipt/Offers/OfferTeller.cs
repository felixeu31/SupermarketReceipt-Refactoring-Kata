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
            if (!offers.ContainsKey(product)) continue;

            var quantity = productQuantities[product];
            var quantityAsInt = (int)quantity;
            var offer = offers[product];
            var unitPrice = catalog.GetUnitPrice(product);

            var discount = CalculateDiscount(offer, quantityAsInt, unitPrice, quantity, product);

            if (discount != null)
                discounts.Add(discount);
        }

        return discounts;
    }

    private static Discount CalculateDiscount(Offer offer, int quantityAsInt, double unitPrice, double quantity, Product product)
    {
        Discount discount = null;
        
        if (offer.OfferType == SpecialOfferType.ThreeForTwo && quantityAsInt > 2)
        {
            var numberOfChunks = quantityAsInt / 3;
            var discountAmount = quantity * unitPrice - (numberOfChunks * 2 * unitPrice + quantityAsInt % 3 * unitPrice);
            return new Discount(product, "3 for 2", -discountAmount);
        }

        var offerCalculator = OfferCalculatorFactory.CreateOfferCalculator(offer.OfferType, offer.Argument);

        return offerCalculator.CalculateDiscount(product, quantityAsInt,
            unitPrice);
    }

    private static Discount CalculatePercentDiscount(Offer offer, double unitPrice, double quantity, Product product)
    {
        return new Discount(product, offer.Argument + "% off", -quantity * unitPrice * offer.Argument / 100.0);
    }
}