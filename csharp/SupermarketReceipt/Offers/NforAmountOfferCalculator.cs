using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;

namespace SupermarketReceipt.Offers
{
    public class NforAmountOfferCalculator : OfferCalculator
    {
        private readonly int _chunkSize;
        private readonly double _amount;

        public NforAmountOfferCalculator(int chunkSize, double amount)
        {
            _chunkSize = chunkSize;
            _amount = amount;
        }

        public Discount CalculateDiscount(Product product, int quantity, double unitPrice)
        {
            var numberOfChunks = quantity / _chunkSize;

            if (numberOfChunks == 0)
            {
                return null;
            }

            var totalPriceWithoutDiscounts = unitPrice * quantity;

            var priceForChunks = _amount * numberOfChunks;
            var priceForRemanentUnits = quantity % _chunkSize * unitPrice;
            var totalPriceWithDiscounts = priceForChunks + priceForRemanentUnits;

            var discountTotal = totalPriceWithoutDiscounts - totalPriceWithDiscounts;

            return new Discount(product, _chunkSize + " for " + _amount, -discountTotal);
        }
    }
}
