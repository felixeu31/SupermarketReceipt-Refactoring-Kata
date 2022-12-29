﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReceipt.Offers
{
    public static class OfferCalculatorFactory
    {
        public static OfferCalculator CreateOfferCalculator(SpecialOfferType offerType, double argument)
        {
            switch (offerType)
            {
                case SpecialOfferType.ThreeForTwo:
                    return null;
                case SpecialOfferType.TenPercentDiscount:
                    return null;
                case SpecialOfferType.TwoForAmount:
                    return new NforAmountOfferCalculator(2, argument);
                case SpecialOfferType.FiveForAmount:
                    return new NforAmountOfferCalculator(5, argument);
                default:
                    throw new ArgumentOutOfRangeException(nameof(offerType), offerType, null);
            }
        }
    }
}
