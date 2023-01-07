using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketReceipt.Offers.OfferCalculators
{
    public class NforMOffer : ProductOffer, IOfferCalculator
    {
        private readonly int _nChunkSize;
        private readonly int _mChunkSize;

        public NforMOffer(Product product, int nChunkSize, int mChunkSize) : base(product)
        {
            _nChunkSize = nChunkSize;
            _mChunkSize = mChunkSize;
        }


        public Discount CalculateDiscount(List<ProductQuantity> productQuantities, SupermarketCatalog catalog)
        {
            var productQuantity = productQuantities.FirstOrDefault(x => Equals(x.Product, _product));

            if (productQuantity == null)
            {
                return null;
            }

            var quantity = (int)productQuantity.Quantity;

            var unitPrice = catalog.GetUnitPrice(_product);

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
