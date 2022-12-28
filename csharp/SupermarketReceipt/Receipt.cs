using System.Collections.Generic;
using System.Linq;

namespace SupermarketReceipt
{
    public class Receipt
    {
        private readonly List<Discount> _discounts = new List<Discount>();
        private readonly List<ReceiptItem> _items = new List<ReceiptItem>();

        public double GetTotalPrice()
        {
            return _items.Sum(x => x.TotalPrice)
                   + _discounts.Sum(x => x.DiscountAmount);
        }

        public void AddProduct(Product p, double quantity, double price)
        {
            _items.Add(new ReceiptItem(p, quantity, price));
        }

        public List<ReceiptItem> GetItems()
        {
            return new List<ReceiptItem>(_items);
        }

        public void AddDiscount(Discount discount)
        {
            _discounts.Add(discount);
        }

        public List<Discount> GetDiscounts()
        {
            return _discounts;
        }
    }

    public class ReceiptItem
    {
        public ReceiptItem(Product p, double quantity, double price)
        {
            Product = p;
            Quantity = quantity;
            Price = price;
        }

        public Product Product { get; }
        public double Price { get; }
        public double TotalPrice => Quantity * Price;

        public double Quantity { get; }
    }
}