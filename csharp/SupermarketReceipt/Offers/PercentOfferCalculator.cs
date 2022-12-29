using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;
using System;

namespace SupermarketReceipt.Offers
{
    public class PercentOfferCalculator : OfferCalculator
    {
        private readonly double _percent;

        public PercentOfferCalculator(double percent)
        {
            _percent = percent;
        }

        public Discount CalculateDiscount(Product product, int quantity, double unitPrice)
        {
            return new Discount(product, _percent + "% off", -quantity * unitPrice * _percent / 100.0);
        }
    }
}
