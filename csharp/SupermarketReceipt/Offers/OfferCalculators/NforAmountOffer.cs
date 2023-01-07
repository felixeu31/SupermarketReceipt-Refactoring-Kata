using System.Collections.Generic;
using System.Linq;
using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;

namespace SupermarketReceipt.Offers.OfferCalculators
{
    public class NforAmountOffer : ProductOffer, IOfferCalculator
    {
        private readonly int _chunkSize;
        private readonly double _amount;

        public NforAmountOffer(Product product, int chunkSize, double amount) : base(product)
        {
            _chunkSize = chunkSize;
            _amount = amount;
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
