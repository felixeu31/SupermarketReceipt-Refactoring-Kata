using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReceipt.Offers.BundleOffers
{
    public class BundleOffer
    {
        protected readonly Bundle Bundle;
        public BundleOffer(Bundle bundle, params ProductQuantity[] productQuantities)
        {
            Bundle = bundle;
        }
    }
}
