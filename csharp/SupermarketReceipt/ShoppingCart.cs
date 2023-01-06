using System.Collections.Generic;
using SupermarketReceipt.Products;

namespace SupermarketReceipt
{
    public class ShoppingCart
    {
        private readonly Dictionary<Product, double> _productQuantities = new();

        public Dictionary<Product, double> GetItems()
        {
            return _productQuantities;
        }

        public void AddItem(Product product)
        {
            AddItemQuantity(product, 1.0);
        }

        public void AddItemQuantity(Product product, double quantity)
        {
            if (_productQuantities.ContainsKey(product))
            {
                var newAmount = _productQuantities[product] + quantity;
                _productQuantities[product] = newAmount;
            }
            else
            {
                _productQuantities.Add(product, quantity);
            }
        }
    }
}