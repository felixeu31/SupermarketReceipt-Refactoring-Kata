using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;
using System.Collections.Generic;

namespace SupermarketReceipt.Offers.OfferCalculators;

public interface IOfferCalculator
{
    Discount CalculateDiscount(List<ProductQuantity> productQuantities, SupermarketCatalog catalog);
}