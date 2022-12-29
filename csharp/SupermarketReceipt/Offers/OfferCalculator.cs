using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;

namespace SupermarketReceipt.Offers;

public interface OfferCalculator
{
    Discount CalculateDiscount(Product product, int quantity, double unitPrice);
}