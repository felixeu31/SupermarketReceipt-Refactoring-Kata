using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace SupermarketReceipt.Test
{
    public class SupermarketTest
    {
        [Fact]
        public void an_empty_cart_should_cost_nothing()
        {
            SupermarketCatalog catalog = new FakeCatalog();

            var cart = new ShoppingCart();

            var teller = new Teller(catalog);

            var receipt = teller.GenerateReceipt(cart);

            receipt.GetTotalPrice().Should().Be(0);
        }

        [Fact]
        public void TenPercentDiscount()
        {
            // ARRANGE
            SupermarketCatalog catalog = new FakeCatalog();
            var toothbrush = new Product("toothbrush", ProductUnit.Each);
            catalog.AddProduct(toothbrush, 0.99);
            var apples = new Product("apples", ProductUnit.Kilo);
            catalog.AddProduct(apples, 1.99);

            var cart = new ShoppingCart();
            cart.AddItemQuantity(apples, 2.5);

            var teller = new Teller(catalog);
            teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, toothbrush, 10.0);

            // ACT
            var receipt = teller.GenerateReceipt(cart);

            // ASSERT
            Assert.Equal(4.975, receipt.GetTotalPrice());
            Assert.Equal(new List<Discount>(), receipt.GetDiscounts());
            Assert.Single(receipt.GetItems());
            var receiptItem = receipt.GetItems()[0];
            Assert.Equal(apples, receiptItem.Product);
            Assert.Equal(1.99, receiptItem.Price);
            Assert.Equal(2.5 * 1.99, receiptItem.TotalPrice);
            Assert.Equal(2.5, receiptItem.Quantity);
        }
    }
}