﻿using SupermarketReceipt.Products;
using SupermarketReceipt.Receipts;

namespace SupermarketReceipt.Offers;

public interface IOfferCalculator
{
    Discount CalculateDiscount(int quantity, double unitPrice);
}