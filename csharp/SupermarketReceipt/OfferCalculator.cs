using System.Collections.Generic;

namespace SupermarketReceipt;

public class OfferCalculator
{
    public static void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers, SupermarketCatalog catalog, Dictionary<Product, double> productQuantities)
    {
        foreach (var p in productQuantities.Keys)
        {
            var quantity = productQuantities[p];
            var quantityAsInt = (int) quantity;
            if (offers.ContainsKey(p))
            {
                var offer = offers[p];
                var unitPrice = catalog.GetUnitPrice(p);
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
                        discount = new Discount(p, "2 for " + offer.Argument, -discountN);
                    }
                }

                if (offer.OfferType == SpecialOfferType.FiveForAmount) x = 5;
                var numberOfXs = quantityAsInt / x;
                if (offer.OfferType == SpecialOfferType.ThreeForTwo && quantityAsInt > 2)
                {
                    var discountAmount = quantity * unitPrice - (numberOfXs * 2 * unitPrice + quantityAsInt % 3 * unitPrice);
                    discount = new Discount(p, "3 for 2", -discountAmount);
                }

                if (offer.OfferType == SpecialOfferType.TenPercentDiscount) discount = new Discount(p, offer.Argument + "% off", -quantity * unitPrice * offer.Argument / 100.0);
                if (offer.OfferType == SpecialOfferType.FiveForAmount && quantityAsInt >= 5)
                {
                    var discountTotal = unitPrice * quantity - (offer.Argument * numberOfXs + quantityAsInt % 5 * unitPrice);
                    discount = new Discount(p, x + " for " + offer.Argument, -discountTotal);
                }

                if (discount != null)
                    receipt.AddDiscount(discount);
            }
        }
    }
}