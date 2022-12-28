using System.Collections.Generic;
using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace SupermarketReceipt.Test
{
    public class SupermarketTest
    {

        private SupermarketCatalog _catalog;
        private Teller _teller;
        //private ShoppingCart _theCart;
        private Product _toothbrush;
        private Product _rice;
        private Product _apples;
        private Product _cherryTomatoes;

        public SupermarketTest()
        {

            _catalog = new FakeCatalog();
            _teller = new Teller(_catalog);
            //_theCart = new ShoppingCart();

            _toothbrush = new Product("toothbrush", ProductUnit.Each);
            _catalog.AddProduct(_toothbrush, 0.99);
            _rice = new Product("rice", ProductUnit.Each);
            _catalog.AddProduct(_rice, 2.99);
            _apples = new Product("apples", ProductUnit.Kilo);
            _catalog.AddProduct(_apples, 1.99);
            _cherryTomatoes = new Product("cherry tomato box", ProductUnit.Each);
            _catalog.AddProduct(_cherryTomatoes, 0.69);
        }

        [Fact]
        public void an_empty_cart_should_cost_nothing()
        {
            var cart = new ShoppingCart();

            var receipt = _teller.GenerateReceipt(cart);

            receipt.GetTotalPrice().Should().Be(0);
        }

        [Fact]
        public void TenPercentDiscount()
        {
            // ARRANGE
            var cart = new ShoppingCart();
            cart.AddItemQuantity(_apples, 2.5);
            _teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, _toothbrush, 10.0);

            // ACT
            var receipt = _teller.GenerateReceipt(cart);

            // ASSERT
            Assert.Equal(4.975, receipt.GetTotalPrice());
            Assert.Equal(new List<Discount>(), receipt.GetDiscounts());
            Assert.Single(receipt.GetItems());
            var receiptItem = receipt.GetItems()[0];
            Assert.Equal(_apples, receiptItem.Product);
            Assert.Equal(1.99, receiptItem.Price);
            Assert.Equal(2.5 * 1.99, receiptItem.TotalPrice);
            Assert.Equal(2.5, receiptItem.Quantity);
        }
    }
}