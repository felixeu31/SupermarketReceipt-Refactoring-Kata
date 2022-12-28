using FluentAssertions;
using Xunit;

namespace SupermarketReceipt.Test;

public class ReceiptTests
{
    [Fact]
    public void GetItems_ReturnAddedItems()
    {
        // Arrange
        Receipt receipt = new Receipt();
        Product product1 = new Product("product1", ProductUnit.Each);
        Product product2 = new Product("product2", ProductUnit.Each);

        // Act
        receipt.AddProduct(product1, 1, 1);
        receipt.AddProduct(product2, 1, 1);

        // Assert
        var receiptItems = receipt.GetItems();
        receiptItems.Should().HaveCount(2);
        receiptItems.Should().Contain(x => x.Product.Name == "product1");
        receiptItems.Should().Contain(x => x.Product.Name == "product2");
    }

    [Fact]
    public void GetDiscount_ReturnAddedDiscounts()
    {
        // Arrange
        Receipt receipt = new Receipt();
        Product product1 = new Product("product1", ProductUnit.Each);
        Product product2 = new Product("product2", ProductUnit.Each);
        Discount discount1 = new Discount(product1, "discount1", 0.1);
        Discount discount2 = new Discount(product1, "discount2", 0.1);

        // Act
        receipt.AddProduct(product1, 1, 1);
        receipt.AddProduct(product2, 1, 1);
        receipt.AddDiscount(discount1);
        receipt.AddDiscount(discount2);

        // Assert
        var receiptDiscounts = receipt.GetDiscounts();
        receiptDiscounts.Should().HaveCount(2);
        receiptDiscounts.Should().Contain(x => x.Description == "discount1");
        receiptDiscounts.Should().Contain(x => x.Description == "discount2");
    }
    
    [Fact]
    public void GetTotalPrice_WhenAddProducts()
    {
        // Arrange
        Receipt receipt = new Receipt();
        Product product1 = new Product("product1", ProductUnit.Each);
        Product product2 = new Product("product2", ProductUnit.Each);

        // Act
        receipt.AddProduct(product1, 1, 1);
        receipt.AddProduct(product2, 1, 1);

        // Assert
        receipt.GetTotalPrice().Should().Be(2);
    }

    [Fact]
    public void GetTotalPrice_CalculatesFromQuantityAndPrice()
    {
        // Arrange
        Receipt receipt = new Receipt();
        Product product1 = new Product("product1", ProductUnit.Each);
        Product product2 = new Product("product2", ProductUnit.Each);

        // Act
        receipt.AddProduct(product1, 2, 1);
        receipt.AddProduct(product2, 1, 2);

        // Assert
        receipt.GetTotalPrice().Should().Be(4);
    }

    [Fact]
    public void GetTotalPrice_WhenAddProductsAndDiscounts()
    {
        // Arrange
        Receipt receipt = new Receipt();
        Product product1 = new Product("product1", ProductUnit.Each);
        Product repeatedProduct1 = new Product("product1", ProductUnit.Each);
        Product product2 = new Product("product2", ProductUnit.Each);
        Discount discount1 = new Discount(product1, "discount1", -0.2);
        Discount discount2 = new Discount(product1, "discount2", -0.2);

        // Act
        receipt.AddProduct(product1, 1, 1);
        receipt.AddProduct(repeatedProduct1, 1, 1);
        receipt.AddProduct(product2, 1, 1);
        receipt.AddDiscount(discount1);
        receipt.AddDiscount(discount2);

        // Assert
        receipt.GetTotalPrice().Should().Be(2.6);
    }
}