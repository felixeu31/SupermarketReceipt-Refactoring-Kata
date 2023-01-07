using SupermarketReceipt.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SupermarketReceipt.Offers
{
    public class ProductOffer
    {
        protected readonly Product _product;

        public ProductOffer(Product product)
        {
            _product = product;
        }
    }
}
