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
        public void one_normal_item()
        {
            var cart = new ShoppingCart();
            cart.AddItemQuantity(_apples, 2.5);

            var receipt = _teller.GenerateReceipt(cart);

            var expectedPrice = 1.99 * 2.5;

            receipt.GetTotalPrice().Should().Be(expectedPrice);
        }

        [Fact]
        public void buy_three_get_one_free()
        {
            var cart = new ShoppingCart();
            cart.AddItem(_toothbrush);
            cart.AddItem(_toothbrush);
            cart.AddItem(_toothbrush);
            _teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, _toothbrush, _catalog.GetUnitPrice(_toothbrush));

            Receipt receipt = _teller.GenerateReceipt(cart);

            receipt.GetTotalPrice().Should().Be(0.99 * 2);
        }


        [Fact]
        public void buy_five_get_one_free()
        {
            var cart = new ShoppingCart();
            cart.AddItem(_toothbrush);
            cart.AddItem(_toothbrush);
            cart.AddItem(_toothbrush);
            cart.AddItem(_toothbrush);
            cart.AddItem(_toothbrush);
            _teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, _toothbrush, _catalog.GetUnitPrice(_toothbrush));

            Receipt receipt = _teller.GenerateReceipt(cart);

            receipt.GetTotalPrice().Should().Be(0.99 * 4);
        }


        [Fact]
        public void percent_discount()
        {
            var cart = new ShoppingCart();
            cart.AddItemQuantity(_toothbrush, 5);
            _teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, _toothbrush, 10.0); 

            var receipt = _teller.GenerateReceipt(cart);

            receipt.GetTotalPrice().Should().Be(4.455);
        }


        [Fact]
        public void two_for_amount()
        {
            var cart = new ShoppingCart();
            cart.AddItemQuantity(_toothbrush, 2);
            _teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, _toothbrush, 1.5);

            var receipt = _teller.GenerateReceipt(cart);

            receipt.GetTotalPrice().Should().Be(1.5);
        }
        [Fact]
        public void two_for_amount_with_five()
        {
            var cart = new ShoppingCart();
            cart.AddItemQuantity(_toothbrush, 5);
            _teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, _toothbrush, 1.5);

            var receipt = _teller.GenerateReceipt(cart);

            receipt.GetTotalPrice().Should().Be(3.99);
        }

        [Fact]
        public void five_for_amount()
        {
            var cart = new ShoppingCart();
            cart.AddItemQuantity(_toothbrush, 5);
            _teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _toothbrush, 3.5);

            var receipt = _teller.GenerateReceipt(cart);

            receipt.GetTotalPrice().Should().Be(3.5);
        }


        [Fact]
        public void five_for_amount_with_four()
        {
            var cart = new ShoppingCart();
            cart.AddItemQuantity(_toothbrush, 4);
            _teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _toothbrush, 3.5);

            var receipt = _teller.GenerateReceipt(cart);

            receipt.GetTotalPrice().Should().Be(3.96);
        }


        [Fact]
        public void five_for_amount_with_six()
        {
            var cart = new ShoppingCart();
            cart.AddItemQuantity(_toothbrush, 6);
            _teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _toothbrush, 3.5);

            var receipt = _teller.GenerateReceipt(cart);

            receipt.GetTotalPrice().Should().Be(4.49);
        }

        [Fact]
        public void five_for_amount_with_ten()
        {
            var cart = new ShoppingCart();
            cart.AddItemQuantity(_toothbrush, 10);
            _teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _toothbrush, 3.5);

            var receipt = _teller.GenerateReceipt(cart);

            receipt.GetTotalPrice().Should().Be(7);
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