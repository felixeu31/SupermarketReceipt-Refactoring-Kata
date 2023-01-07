
# Initial test coverage 
- ReceiptTests
- SupermarketTest (pitch point that tests several classes at the same time)

# Identified smells, refactors, fixes...

- [Done] Receipt
  - [Done] Passing a totalPrice parameter apart from quantity and price might led to inconsistent data

- [Done] ShoppingCart.HandleOffers has many dependencies (receipt, ...). 
  - [Done] It silently mutates the state of the passed Receipt instance (difficult to catch before goiing through the actual impmlementation)
  - [Done] It doesn't seem right that the Sopping cart having the responsability of changing the receipt

- [Done] Teller implementation has bad naming

- [Done]OfferTeller logic is very difficult to understand
  - [Done] Move each offer calculator to specific class
  - [Done] Create factory of offer calculators
  - [Done] Use polimorfism to calculate discount


# New feature
- It is required a new type of offer for bundles
  - It has a big difference between normal offers because it is not related to a single product
  - It is time now to decide what kind of abstraction fits better for the purpose of this new type of offer. 
    - It is a different type of object? Does it have any similiraty with the actual offer object.
    - It is a good idea to start creating a new type of object and then see what are the similarities?