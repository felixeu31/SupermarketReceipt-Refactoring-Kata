# Identified smells

- Receipt
  - Passing a totalPrice parameter apart from quantity and price might led to inconsistent data

- ShoppingCart.HandleOffers has many dependencies (receipt, ...). 
  - It silently mutates the state of the passed Receipt instance (difficult to catch before goiing through the actual impmlementation)
  - It doesn't seem right that the Sopping cart having the responsability of changing the receipt
- Teller implementation has bad naming

# Initial test coverage 
- ReceiptTests
- 