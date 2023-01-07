﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReceipt.Offers.OfferCalculators
{
    public static class OfferCalculatorFactory
    {
        public static IOfferCalculator CreateOfferCalculator(Offer offer)
        {
            switch (offer.OfferType)
            {
                case SpecialOfferType.ThreeForTwo:
                    return new NforMOffer(offer.Product, 3, 2);
                case SpecialOfferType.TenPercentDiscount:
                    return new PercentOffer(offer.Product, 10.0);
                case SpecialOfferType.TwoForAmount:
                    return new NforAmountOffer(offer.Product, 2, offer.Argument);
                case SpecialOfferType.FiveForAmount:
                    return new NforAmountOffer(offer.Product, 5, offer.Argument);
                default:
                    throw new ArgumentOutOfRangeException(nameof(offer.OfferType), offer.OfferType, null);
            }
        }
    }
}
