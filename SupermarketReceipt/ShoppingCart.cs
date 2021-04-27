using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SupermarketReceipt
{
    public class ShoppingCart
    {
        private readonly Items _items;

        private ShoppingCart(Items items) :this()
        {
            _items = items;
        }

        public ShoppingCart()
        {
            _items = new Items();
        }
        public List<ProductQuantity> GetItems()
        {
            return new List<ProductQuantity>(_items.GetAll());
        }

        public ShoppingCart AddItemQuantity(Product product, double quantity)
        {
            return new ShoppingCart(_items.Add(new ProductQuantity(product,quantity)));
        }

        public void HandleOffers(Receipt receipt, Dictionary<Product, Offer> offers, SupermarketCatalog catalog)
        {
            var _productQuantities = ProductQuantities();
            foreach (var p in _productQuantities.Keys)
            {
                var quantity = _productQuantities[p];
                var quantityAsInt = (int) quantity;
                if (offers.ContainsKey(p))
                {
                    var offer = offers[p];
                    var unitPrice = catalog.GetUnitPrice(p);
                    Discount discount = null;
                    var x = 1;
                    if (offer.OfferType == SpecialOfferType.ThreeForTwo)
                    {
                        x = 3;
                    }
                    else if (offer.OfferType == SpecialOfferType.TwoForAmount)
                    {
                        x = 2;
                        if (quantityAsInt >= 2)
                        {
                            var total = offer.Argument * (quantityAsInt / x) + quantityAsInt % 2 * unitPrice;
                            var discountN = unitPrice * quantity - total;
                            discount = new Discount(p, "2 for " + offer.Argument, -discountN);
                        }
                    }

                    if (offer.OfferType == SpecialOfferType.FiveForAmount) x = 5;
                    var numberOfXs = quantityAsInt / x;
                    if (offer.OfferType == SpecialOfferType.ThreeForTwo && quantityAsInt > 2)
                    {
                        var discountAmount = quantity * unitPrice - (numberOfXs * 2 * unitPrice + quantityAsInt % 3 * unitPrice);
                        discount = new Discount(p, "3 for 2", -discountAmount);
                    }

                    if (offer.OfferType == SpecialOfferType.TenPercentDiscount) discount = new Discount(p, offer.Argument + "% off", -quantity * unitPrice * offer.Argument / 100.0);
                    if (offer.OfferType == SpecialOfferType.FiveForAmount && quantityAsInt >= 5)
                    {
                        var discountTotal = unitPrice * quantity - (offer.Argument * numberOfXs + quantityAsInt % 5 * unitPrice);
                        discount = new Discount(p, x + " for " + offer.Argument, -discountTotal);
                    }

                    if (discount != null)
                        receipt.AddDiscount(discount);
                }
            }
        }

        private Dictionary<Product,double > ProductQuantities()
        {
            return _items
                .GetAll()
                .GroupBy(l => l.Product)
                .ToDictionary(x => x.Key, x => x.Sum(y => y.Quantity));
        }
    }
}