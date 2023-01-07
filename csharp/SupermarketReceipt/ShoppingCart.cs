using System.Collections.Generic;
using System.Linq;
using SupermarketReceipt.Products;

namespace SupermarketReceipt
{
    public class ShoppingCart
    {
        private readonly List<ProductQuantity> _productQuantities = new();

        public List<ProductQuantity> GetItems()
        {
            return _productQuantities;
        }

        public void AddItem(Product product)
        {
            AddItemQuantity(product, 1.0);
        }

        public void AddItemQuantity(Product product, double quantity)
        {
            var existingProduct = _productQuantities.FirstOrDefault(x => Equals(x.Product, product));

            if (existingProduct != null)
            {
                existingProduct.AddQuantity(quantity);
            }
            else
            {
                _productQuantities.Add(new ProductQuantity(product, quantity));
            }
        }
    }
}