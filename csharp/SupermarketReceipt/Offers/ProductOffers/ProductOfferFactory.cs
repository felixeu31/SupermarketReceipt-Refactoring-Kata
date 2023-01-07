using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupermarketReceipt.Products;

namespace SupermarketReceipt.Offers.OfferCalculators
{
    public static class ProductOfferFactory
    {
        public static IOffer CreateOfferCalculator(OfferType offerType, Product product, double argument)
        {
            switch (offerType)
            {
                case OfferType.ThreeForTwo:
                    return new NforMOffer(product, 3, 2);
                case OfferType.TenPercentDiscount:
                    return new PercentOffer(product, 10.0);
                case OfferType.TwoForAmount:
                    return new NforAmountOffer(product, 2, argument);
                case OfferType.FiveForAmount:
                    return new NforAmountOffer(product, 5, argument);
                default:
                    throw new ArgumentOutOfRangeException(nameof(offerType), offerType, null);
            }
        }
    }
}
