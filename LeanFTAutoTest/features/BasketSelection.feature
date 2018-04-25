#Auto generated Octane revision tag
@TID1001REV0.2.0
Feature: BasketSelection
	As a customer
	I want to be able to add products to my basket

Scenario: Adding Item to Basket
	Given I am on the shop web site
	When I add an item to the basket
	Then I can see the item in the basket
