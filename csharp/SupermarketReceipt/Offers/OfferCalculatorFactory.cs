using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReceipt.Offers
{
    public static class OfferCalculatorFactory
    {
        public static OfferCalculator CreateOfferCalculator(Offer offer)
        {
            switch (offer.OfferType)
            {
                case SpecialOfferType.ThreeForTwo:
                    return new NforMOfferCalculator(3, 2);
                case SpecialOfferType.TenPercentDiscount:
                    return new PercentOfferCalculator(10.0);
                case SpecialOfferType.TwoForAmount:
                    return new NforAmountOfferCalculator(2, offer.Argument);
                case SpecialOfferType.FiveForAmount:
                    return new NforAmountOfferCalculator(5, offer.Argument);
                default:
                    throw new ArgumentOutOfRangeException(nameof(offer.OfferType), offer.OfferType, null);
            }
        }
    }
}
