using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Combinations;
using System.Collections.Generic;
using Xunit;

[assembly: UseReporter(typeof(DiffReporter))]

namespace SupermarketReceipt.Test
{
    public class SupermarketTest
    {
        private readonly SupermarketCatalog _catalog;
        private readonly Product _toothbrush;
        private readonly Product _apples;

        public SupermarketTest()
        {
            _catalog = new FakeCatalog();
            _toothbrush = new Product("toothbrush", ProductUnit.Each);
            _catalog.AddProduct(_toothbrush, 0.99);
            _apples = new Product("apples", ProductUnit.Kilo);
            _catalog.AddProduct(_apples, 1.99);
        }

        [Fact]
        public void GoldenMasterTest()
        {
            List<double> dataSet = new List<double> { 2, 0, 1.5, 5 ,0.1, -1 };
            CombinationApprovals.VerifyAllCombinations((double applesQuantity1, double applesQuantity2, double toothbrushQuantity1, double toothbrushQuantity2) =>
            {
                var cart = InitializeCart(applesQuantity1, applesQuantity2, toothbrushQuantity1, toothbrushQuantity2);
                var teller = new Teller(_catalog);
                
                var receipt = teller.ChecksOutArticlesFrom(cart);

                return new ReceiptPrinter().PrintReceipt(receipt);
            }, dataSet, dataSet, dataSet, dataSet);
        }

        private ShoppingCart InitializeCart(double applesQuantity1, double applesQuantity2, double toothbrushQuantity1, double toothbrushQuantity2)
        {
            var cart = new ShoppingCart();
            if (applesQuantity1 != 0)
                cart = cart.AddItemQuantity(_apples, applesQuantity1);

            if (toothbrushQuantity1 != 0)
                cart = cart.AddItemQuantity(_toothbrush, toothbrushQuantity1);
            
            if (applesQuantity2 != 0)
                cart = cart.AddItemQuantity(_apples, applesQuantity2);

            if (toothbrushQuantity2 != 0)
                cart = cart.AddItemQuantity(_toothbrush, toothbrushQuantity2);
            
            return cart;
        }

        private ShoppingCart InitializeCart(double applesQuantity, double toothbrushQuantity)
        {
            var cart = new ShoppingCart();
            if (applesQuantity != 0)
                cart = cart.AddItemQuantity(_apples, applesQuantity);

            if (toothbrushQuantity != 0)
                cart = cart.AddItemQuantity(_toothbrush, toothbrushQuantity);
            
            return cart;
        }

        [Fact]
        public void GoldenMasterThreeForTwoReductionTest()
        {
            List<double> productDataSet = new List<double> { 0,1 ,2 ,3 ,5, 6 ,-3 };
            List<double> argumentDataSet = new List<double> { 2, 0, 1.5, 5 ,0.1, -1 };
            CombinationApprovals.VerifyAllCombinations((double applesQuantity1, double applesQuantity2, double toothbrushQuantity1, double toothbrushQuantity2, double argument) =>
            {
                var cart = InitializeCart(applesQuantity1, applesQuantity2, toothbrushQuantity1, toothbrushQuantity2);
                var teller = new Teller(_catalog);
                teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, _apples, argument);
                teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, _toothbrush, argument);
                
                var receipt = teller.ChecksOutArticlesFrom(cart);

                return new ReceiptPrinter().PrintReceipt(receipt);
            }, productDataSet, productDataSet, productDataSet, productDataSet,argumentDataSet);
        }

        [Fact]
        public void GoldenMasterTenPercentDiscountReductionTest()
        {
            List<double> productDataSet = new List<double> { 0,1 ,2 ,3 ,5, 6 ,-3 };
            List<double> argumentDataSet = new List<double> { 2, 0, 1.5, 5 ,0.1, -1 };
            CombinationApprovals.VerifyAllCombinations((double applesQuantity1, double applesQuantity2, double toothbrushQuantity1, double toothbrushQuantity2, double argument) =>
            {
                var cart = InitializeCart(applesQuantity1, applesQuantity2, toothbrushQuantity1, toothbrushQuantity2);
                var teller = new Teller(_catalog);
                teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, _apples, argument);
                teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, _toothbrush, argument);
                
                var receipt = teller.ChecksOutArticlesFrom(cart);

                return new ReceiptPrinter().PrintReceipt(receipt);
            }, productDataSet, productDataSet, productDataSet, productDataSet,argumentDataSet);
        }

        [Fact]
        public void GoldenMasterTwoForAmountDiscountReductionTest()
        {
            List<double> productDataSet = new List<double> { 0,1 ,2 ,3 ,5, 6 ,-3 };
            List<double> argumentDataSet = new List<double> { 2, 0, 1.5, 5 ,0.1, -1 };
            CombinationApprovals.VerifyAllCombinations((double applesQuantity1, double applesQuantity2, double toothbrushQuantity1, double toothbrushQuantity2, double argument) =>
            {
                var cart = InitializeCart(applesQuantity1, applesQuantity2, toothbrushQuantity1, toothbrushQuantity2);
                var teller = new Teller(_catalog);
                teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, _apples, argument);
                teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, _toothbrush, argument);
                
                var receipt = teller.ChecksOutArticlesFrom(cart);

                return new ReceiptPrinter().PrintReceipt(receipt);
            }, productDataSet, productDataSet, productDataSet, productDataSet,argumentDataSet);
        }

        [Fact]
        public void GoldenMasterFiveForAmountDiscountReductionTest()
        {
            List<double> productDataSet = new List<double> { 0,1 ,2 ,3 ,5,9 ,10 ,-5 };
            List<double> argumentDataSet = new List<double> { 2, 0, 1.5, 5 ,0.1, -1 };
            CombinationApprovals.VerifyAllCombinations((double applesQuantity1, double applesQuantity2, double toothbrushQuantity1, double toothbrushQuantity2, double argument) =>
            {
                var cart = InitializeCart(applesQuantity1, applesQuantity2, toothbrushQuantity1, toothbrushQuantity2);
                var teller = new Teller(_catalog);
                teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _apples, argument);
                teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, _toothbrush, argument);
                
                var receipt = teller.ChecksOutArticlesFrom(cart);

                return new ReceiptPrinter().PrintReceipt(receipt);
            }, productDataSet, productDataSet, productDataSet, productDataSet,argumentDataSet);
        }
    }
}