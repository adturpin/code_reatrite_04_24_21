using System;
using System.Collections.Generic;

namespace SupermarketReceipt
{
    public class ShoppingCart
    {
        private readonly List<ProductQuantity> _items = new List<ProductQuantity>();
        private readonly Dictionary<Product, double> _productQuantities = new Dictionary<Product, double>();


        public List<ProductQuantity> GetItems()
        {
            return new List<ProductQuantity>(_items);
        }

        public void AddItem(Product product)
        {
            AddItemQuantity(product, 1.0);
        }


        public void AddItemQuantity(Product product, double quantity)
        {
            _items.Add(new ProductQuantity(product, quantity));
            if (_productQuantities.ContainsKey(product))
            {
                var newAmount = _productQuantities[product] + quantity;
                _productQuantities[product] = newAmount;
            }
            else
            {
                _productQuantities.Add(product, quantity);
            }
        }

        public void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers, SupermarketCatalog catalog)
        {
            foreach (var productKey in _productQuantities.Keys)
            {
                var quantity = _productQuantities[productKey];
                if (offers.ContainsKey(productKey))
                {
                    var offer = offers[productKey];
                    var unitPrice = catalog.GetUnitPrice(productKey);
                    Discount discount = null;

                    if (offer.OfferType == SpecialOfferType.TwoForAmount)
                    {
                        discount = TwoForAmount(offer, unitPrice, quantity, productKey);
                    }

                    if (offer.OfferType == SpecialOfferType.ThreeForTwo)
                    {
                        // missing offer
                        discount = ThreeForTwo( quantity, unitPrice, productKey, null);
                    }

                    if (offer.OfferType == SpecialOfferType.TenPercentDiscount)
                    {
                        discount = TenPercentDiscount(productKey, offer, quantity, unitPrice);
                    }

                    if (offer.OfferType == SpecialOfferType.FiveForAmount)
                    {
                        discount = FiveForAmount(unitPrice, quantity, offer, productKey);
                    }

                    if (discount != null)
                        receipt.AddDiscount(discount);
                }
            }
        }

        private static Discount FiveForAmount( double unitPrice, double quantity, Offer offer,
             Product p)
        {
            Discount discount = null;
            int quantityAsInt = (int) quantity;
            if (quantityAsInt >= 5)
            {
                var discountTotal =
                    unitPrice * quantity - (offer.Argument * (quantityAsInt / 5) + quantityAsInt % 5 * unitPrice);
                discount = new Discount(p, 5 + " for " + offer.Argument, -discountTotal);
            }

            return discount;
        }

        private static Discount TenPercentDiscount(Product p, Offer offer, double quantity, double unitPrice)
        {
            return new Discount(p, offer.Argument + "% off", -quantity * unitPrice * offer.Argument / 100.0);
        }

        private static Discount ThreeForTwo( double quantity, double unitPrice,  Product p, Offer offer)
        {
            Discount discount = null;
            int quantityAsInt = (int) quantity;
            if (quantityAsInt >= 3)
            {
                var discountAmount =
                    quantity * unitPrice - ((quantityAsInt / 3) * 2 * unitPrice + quantityAsInt % 3 * unitPrice);
                discount = new Discount(p, "3 for 2", -discountAmount);
            }

            return discount;
        }

        private static Discount TwoForAmount( Offer offer, double unitPrice, double quantity,
            Product p)
        {
            Discount discount = null;
            int quantityAsInt = (int) quantity;
            if (quantityAsInt >= 2)
            {
                var total = offer.Argument * (quantityAsInt / 2) + quantityAsInt % 2 * unitPrice;
                var discountN = unitPrice * quantity - total;
                discount = new Discount(p, "2 for " + offer.Argument, -discountN);
            }

            return discount;
        }
    }
}