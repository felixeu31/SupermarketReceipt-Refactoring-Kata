using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;

namespace SupermarketReceipt.Offers
{
    public class NforAmountOfferCalculator : IOfferCalculator
    {
        private readonly Product _product;
        private readonly int _chunkSize;
        private readonly double _amount;

        public NforAmountOfferCalculator(Product product, int chunkSize, double amount)
        {
            _product = product;
            _chunkSize = chunkSize;
            _amount = amount;
        }

        public Discount CalculateDiscount(int quantity, double unitPrice)
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

            return new Discount(_product, _chunkSize + " for " + _amount, -discountTotal);
        }
    }
}
