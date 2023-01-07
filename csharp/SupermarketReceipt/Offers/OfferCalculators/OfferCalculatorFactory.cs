using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SupermarketReceipt.Products;

namespace SupermarketReceipt.Offers.OfferCalculators
{
    public static class OfferCalculatorFactory
    {
        public static IOfferCalculator CreateOfferCalculator(SpecialOfferType offerType, Product product, double argument)
        {
            switch (offerType)
            {
                case SpecialOfferType.ThreeForTwo:
                    return new NforMOffer(product, 3, 2);
                case SpecialOfferType.TenPercentDiscount:
                    return new PercentOffer(product, 10.0);
                case SpecialOfferType.TwoForAmount:
                    return new NforAmountOffer(product, 2, argument);
                case SpecialOfferType.FiveForAmount:
                    return new NforAmountOffer(product, 5, argument);
                default:
                    throw new ArgumentOutOfRangeException(nameof(offerType), offerType, null);
            }
        }
    }
}
