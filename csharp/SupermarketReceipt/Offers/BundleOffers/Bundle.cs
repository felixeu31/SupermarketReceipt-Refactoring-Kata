using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReceipt.Offers.BundleOffers
{
    public class Bundle
    {
        public List<ProductQuantity> ProductsQuantities { get; }
        public Bundle(params ProductQuantity[] productQuantities)
        {
            ProductsQuantities = productQuantities.ToList();
        }
    }
}
