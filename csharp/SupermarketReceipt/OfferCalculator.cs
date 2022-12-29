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


        if (offer.OfferType == SpecialOfferType.TenPercentDiscount)
        {
            return CalculatePercentDiscount(offer, unitPrice, quantity, product);
        }

        var chunkSize = offer.OfferType switch
        {
            SpecialOfferType.ThreeForTwo => 3,
            SpecialOfferType.TwoForAmount => 2,
            SpecialOfferType.FiveForAmount => 5,
            _ => 1
        };

        var numberOfChunks = quantityAsInt / chunkSize;

        if (offer.OfferType == SpecialOfferType.ThreeForTwo && quantityAsInt > 2)
        {
            var discountAmount = quantity * unitPrice - (numberOfChunks * 2 * unitPrice + quantityAsInt % 3 * unitPrice);
            discount = new Discount(product, "3 for 2", -discountAmount);
        }

        if (offer.OfferType is SpecialOfferType.TwoForAmount or SpecialOfferType.FiveForAmount)
        {
            return new NforAmountOfferCalculator(chunkSize, offer.Argument).CalculateDiscount(product, quantityAsInt,
                unitPrice);
        }

        return discount;
    }

    private static Discount CalculatePercentDiscount(Offer offer, double unitPrice, double quantity, Product product)
    {
        return new Discount(product, offer.Argument + "% off", -quantity * unitPrice * offer.Argument / 100.0);
    }
}