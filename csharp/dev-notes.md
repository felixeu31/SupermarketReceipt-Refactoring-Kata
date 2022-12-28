# Identified smells

- Receipt
  - [Done] Passing a totalPrice parameter apart from quantity and price might led to inconsistent data

- [Done] ShoppingCart.HandleOffers has many dependencies (receipt, ...). 
  - [Done] It silently mutates the state of the passed Receipt instance (difficult to catch before goiing through the actual impmlementation)
  - [Done] It doesn't seem right that the Sopping cart having the responsability of changing the receipt
- [Done] Teller implementation has bad naming
- OfferCalculator logic is very difficult to understand

# Initial test coverage 
- ReceiptTests
- 

