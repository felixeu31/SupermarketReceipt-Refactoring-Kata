using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;
using System;

namespace SupermarketReceipt.Offers
{
    public class PercentOfferCalculator : IOfferCalculator
    {
        private readonly Product _product;
        private readonly double _percent;

        public PercentOfferCalculator(Product product, double percent)
        {
            _product = product;
            _percent = percent;
        }

        public Discount CalculateDiscount(int quantity, double unitPrice)
        {
            return new Discount(_product, _percent + "% off", -quantity * unitPrice * _percent / 100.0);
        }
    }
}
