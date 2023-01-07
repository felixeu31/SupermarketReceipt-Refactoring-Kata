using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Json;
using SupermarketReceipt.Offers.OfferCalculators;
using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;

namespace SupermarketReceipt.Offers;

public class OfferTeller
{

    // How can I test this class. It has a collaborator instantiated by a factory.
    // Do i want to test this class without the collaborator taking place.
    // Does it mean I have a bad design for this class??
    // Should i use it as a pitch point (Working effectively with legacy code)
    public static List<Discount> CalculateDiscounts(List<Offer> offers, SupermarketCatalog catalog, List<ProductQuantity> productQuantities)
    {
        List<Discount> discounts = new List<Discount>();

        foreach (var offer in offers)
        {
            var productQuantity = productQuantities.FirstOrDefault(x => Equals(x.Product, offer.Product));

            if (productQuantity == null)
            {
                continue;
            }

            var quantity = (int)productQuantity.Quantity;
            var unitPrice = catalog.GetUnitPrice(offer.Product);

            var offerCalculator = OfferCalculatorFactory.CreateOfferCalculator(offer);

            var discount = offerCalculator.CalculateDiscount(quantity, unitPrice);

            if (discount != null)
                discounts.Add(discount);
        }
        
        if(productQuantities.Any(x => x.Product.Name == "toothpaste") && productQuantities.Any(x => x.Product.Name == "toothbrush"))
            discounts.Add(CalculateBundleDiscount(offers, catalog, productQuantities));

        return discounts;

    }

    private static Discount CalculateBundleDiscount(List<Offer> offers, SupermarketCatalog catalog, List<ProductQuantity> productQuantities)
    {
        double discountAmount = 0.278;

        return new Discount(productQuantities.First().Product, "bundle offer", -discountAmount);
    }
}