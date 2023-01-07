using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;
using System.Collections.Generic;

namespace SupermarketReceipt.Offers;

public interface IOffer
{
    Discount CalculateDiscount(List<ProductQuantity> productQuantities, SupermarketCatalog catalog);
}