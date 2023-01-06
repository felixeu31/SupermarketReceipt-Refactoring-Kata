using System.Collections.Generic;
using System.Linq;
using SupermarketReceipt.Offers.OfferCalculators;
using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;

namespace SupermarketReceipt.Offers;

public class OfferTeller
{

    // How can I test this class. It has a collaborator instantiated by a factory.
    // Do i want to test this class without the collaborator taking place.
    // Does it mean I have a bad design for this class??
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
        
        if(productQuantities.Any(x => x.Key.Name == "toothpaste") && productQuantities.Any(x => x.Key.Name == "toothbrush"))
            discounts.Add(CalculateBundleDiscount(offers, catalog, productQuantities));

        return discounts;

    }

    private static Discount CalculateBundleDiscount(Dictionary<Product, Offer> offers, SupermarketCatalog catalog, Dictionary<Product, double> productQuantities)
    {
        double discountAmount = 0.278;

        return new Discount(productQuantities.First().Key, "bundle offer", -discountAmount);
    }
}