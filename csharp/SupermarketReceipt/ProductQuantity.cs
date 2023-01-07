using SupermarketReceipt.Products;

namespace SupermarketReceipt;

public class ProductQuantity
{
    public Product Product { get; }
    public double Quantity { get; private set; }

    public ProductQuantity(Product product, double quantity)
    {
        Product = product;
        Quantity = quantity;
    }

    public void AddQuantity(double quantity)
    {
        Quantity += quantity;
    }
}