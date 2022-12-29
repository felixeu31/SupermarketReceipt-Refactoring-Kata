using System.Collections.Generic;

namespace SupermarketReceipt;

public class OfferCalculator
{
    public static List<Discount> CalculateDiscounts(Dictionary<Product, Offer> offers, SupermarketCatalog catalog, Dictionary<Product, double> productQuantities)
    {
        List<Discount> discounts = new List<Discount>();

        foreach (var product in productQuantities.Keys)
        {
            if (!offers.ContainsKey(product)) continue;

            var quantity = productQuantities[product];
            var quantityAsInt = (int) quantity;
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
        var x = 1;
        if (offer.OfferType == SpecialOfferType.ThreeForTwo)
        {
            x = 3;
        }
        else if (offer.OfferType == SpecialOfferType.TwoForAmount)
        {
            x = 2;
            if (quantityAsInt >= 2)
            {
                var total = offer.Argument * (quantityAsInt / x) + quantityAsInt % 2 * unitPrice;
                var discountN = unitPrice * quantity - total;
                discount = new Discount(product, "2 for " + offer.Argument, -discountN);
            }
        }

        if (offer.OfferType == SpecialOfferType.FiveForAmount) x = 5;
        var numberOfXs = quantityAsInt / x;
        if (offer.OfferType == SpecialOfferType.ThreeForTwo && quantityAsInt > 2)
        {
            var discountAmount = quantity * unitPrice - (numberOfXs * 2 * unitPrice + quantityAsInt % 3 * unitPrice);
            discount = new Discount(product, "3 for 2", -discountAmount);
        }

        if (offer.OfferType == SpecialOfferType.TenPercentDiscount)
            discount = new Discount(product, offer.Argument + "% off", -quantity * unitPrice * offer.Argument / 100.0);
        if (offer.OfferType == SpecialOfferType.FiveForAmount && quantityAsInt >= 5)
        {
            var discountTotal = unitPrice * quantity - (offer.Argument * numberOfXs + quantityAsInt % 5 * unitPrice);
            discount = new Discount(product, x + " for " + offer.Argument, -discountTotal);
        }

        return discount;
    }
}