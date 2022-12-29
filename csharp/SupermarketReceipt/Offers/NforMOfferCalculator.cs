using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;

namespace SupermarketReceipt.Offers
{
    public class NforMOfferCalculator : OfferCalculator
    {
        private readonly int _nChunkSize;
        private readonly int _mChunkSize;

        public NforMOfferCalculator(int nChunkSize, int mChunkSize)
        {
            _nChunkSize = nChunkSize;
            _mChunkSize = mChunkSize;
        }

        public Discount CalculateDiscount(Product product, int quantity, double unitPrice)
        {
            var numberOfChunks = quantity / _nChunkSize;

            if (numberOfChunks == 0)
            {
                return null;
            }

            var discountAmount = quantity * unitPrice - (numberOfChunks * _mChunkSize * unitPrice + quantity % _nChunkSize * unitPrice);
            return new Discount(product, _nChunkSize + " for " + _mChunkSize, -discountAmount);
        }
    }
}
