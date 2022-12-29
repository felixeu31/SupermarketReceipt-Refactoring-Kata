using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;

namespace SupermarketReceipt.Offers
{
    public class NforMOfferCalculator : IOfferCalculator
    {
        private readonly Product _product;
        private readonly int _nChunkSize;
        private readonly int _mChunkSize;

        public NforMOfferCalculator(Product product, int nChunkSize, int mChunkSize)
        {
            _product = product;
            _nChunkSize = nChunkSize;
            _mChunkSize = mChunkSize;
        }
        

        public Discount CalculateDiscount(int quantity, double unitPrice)
        {
            var numberOfChunks = quantity / _nChunkSize;

            if (numberOfChunks == 0)
            {
                return null;
            }

            var discountAmount = quantity * unitPrice - (numberOfChunks * _mChunkSize * unitPrice + quantity % _nChunkSize * unitPrice);
            return new Discount(_product, _nChunkSize + " for " + _mChunkSize, -discountAmount);
        }
    }
}
