using ApprovalTests;
using ApprovalTests.Reporters;
using ApprovalTests.Combinations;
using System.Collections.Generic;
using Xunit;

namespace SupermarketReceipt.Test
{
    public class SupermarketTest
    {
        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void GoldenMasterTest()
        {
            List<double> dataSet = new List<double> { 2, 0, 1.5, 5 ,0.1, -1 };
            CombinationApprovals.VerifyAllCombinations((double applesQuantity, double toothbrushQuantity) =>
            {

                // ARRANGE
                SupermarketCatalog catalog = new FakeCatalog();
                var toothbrush = new Product("toothbrush", ProductUnit.Each);
                catalog.AddProduct(toothbrush, 0.99);
                var apples = new Product("apples", ProductUnit.Kilo);
                catalog.AddProduct(apples, 1.99);

                var cart = new ShoppingCart();
                if (applesQuantity != 0)
                    cart.AddItemQuantity(apples, applesQuantity); 

                if (toothbrushQuantity != 0)
                cart.AddItemQuantity(toothbrush, toothbrushQuantity);

                var teller = new Teller(catalog);
                //teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, toothbrush, 10.0);

                // ACT
                var receipt = teller.ChecksOutArticlesFrom(cart);

                // ASSERT
                string result = new ReceiptPrinter().PrintReceipt(receipt);
                //Approvals.Verify(result);
                return result;
            }, dataSet, dataSet);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void GoldenMasterThreeForTwoReductionTest()
        {
            List<double> productDataSet = new List<double> { 0,1 ,2 ,3 ,5, 6 ,-3 };
            List<double> argumentDataSet = new List<double> { 2, 0, 1.5, 5 ,0.1, -1 };
            CombinationApprovals.VerifyAllCombinations((double applesQuantity, double toothbrushQuantity, double argument) =>
            {

                // ARRANGE
                SupermarketCatalog catalog = new FakeCatalog();
                var toothbrush = new Product("toothbrush", ProductUnit.Each);
                catalog.AddProduct(toothbrush, 0.99);
                var apples = new Product("apples", ProductUnit.Kilo);
                catalog.AddProduct(apples, 1.99);

                var cart = new ShoppingCart();
                if (applesQuantity != 0)
                    cart.AddItemQuantity(apples, applesQuantity); 

                if (toothbrushQuantity != 0)
                cart.AddItemQuantity(toothbrush, toothbrushQuantity);

                var teller = new Teller(catalog);
                teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, apples, argument);
                teller.AddSpecialOffer(SpecialOfferType.ThreeForTwo, toothbrush, argument);

                // ACT
                var receipt = teller.ChecksOutArticlesFrom(cart);

                // ASSERT
                string result = new ReceiptPrinter().PrintReceipt(receipt);
                //Approvals.Verify(result);
                return result;
            }, productDataSet, productDataSet,argumentDataSet);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void GoldenMasterTenPercentDiscountReductionTest()
        {
            List<double> productDataSet = new List<double> { 0,1 ,2 ,3 ,5, 6 ,-3 };
            List<double> argumentDataSet = new List<double> { 2, 0, 1.5, 5 ,0.1, -1 };
            CombinationApprovals.VerifyAllCombinations((double applesQuantity, double toothbrushQuantity, double argument) =>
            {

                // ARRANGE
                SupermarketCatalog catalog = new FakeCatalog();
                var toothbrush = new Product("toothbrush", ProductUnit.Each);
                catalog.AddProduct(toothbrush, 0.99);
                var apples = new Product("apples", ProductUnit.Kilo);
                catalog.AddProduct(apples, 1.99);

                var cart = new ShoppingCart();
                if (applesQuantity != 0)
                    cart.AddItemQuantity(apples, applesQuantity); 

                if (toothbrushQuantity != 0)
                cart.AddItemQuantity(toothbrush, toothbrushQuantity);

                var teller = new Teller(catalog);
                teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, apples, argument);
                teller.AddSpecialOffer(SpecialOfferType.TenPercentDiscount, toothbrush, argument);

                // ACT
                var receipt = teller.ChecksOutArticlesFrom(cart);

                // ASSERT
                string result = new ReceiptPrinter().PrintReceipt(receipt);
                //Approvals.Verify(result);
                return result;
            }, productDataSet, productDataSet,argumentDataSet);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void GoldenMasterTwoForAmountDiscountReductionTest()
        {
            List<double> productDataSet = new List<double> { 0,1 ,2 ,3 ,5, 6 ,-3 };
            List<double> argumentDataSet = new List<double> { 2, 0, 1.5, 5 ,0.1, -1 };
            CombinationApprovals.VerifyAllCombinations((double applesQuantity, double toothbrushQuantity, double argument) =>
            {

                // ARRANGE
                SupermarketCatalog catalog = new FakeCatalog();
                var toothbrush = new Product("toothbrush", ProductUnit.Each);
                catalog.AddProduct(toothbrush, 0.99);
                var apples = new Product("apples", ProductUnit.Kilo);
                catalog.AddProduct(apples, 1.99);

                var cart = new ShoppingCart();
                if (applesQuantity != 0)
                    cart.AddItemQuantity(apples, applesQuantity); 

                if (toothbrushQuantity != 0)
                cart.AddItemQuantity(toothbrush, toothbrushQuantity);

                var teller = new Teller(catalog);
                teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, apples, argument);
                teller.AddSpecialOffer(SpecialOfferType.TwoForAmount, toothbrush, argument);

                // ACT
                var receipt = teller.ChecksOutArticlesFrom(cart);

                // ASSERT
                string result = new ReceiptPrinter().PrintReceipt(receipt);
                //Approvals.Verify(result);
                return result;
            }, productDataSet, productDataSet,argumentDataSet);
        }

        [Fact]
        [UseReporter(typeof(DiffReporter))]
        public void GoldenMasterFiveForAmountDiscountReductionTest()
        {
            List<double> productDataSet = new List<double> { 0,1 ,2 ,3 ,5,9 ,10 ,-5 };
            List<double> argumentDataSet = new List<double> { 2, 0, 1.5, 5 ,0.1, -1 };
            CombinationApprovals.VerifyAllCombinations((double applesQuantity, double toothbrushQuantity, double argument) =>
            {

                // ARRANGE
                SupermarketCatalog catalog = new FakeCatalog();
                var toothbrush = new Product("toothbrush", ProductUnit.Each);
                catalog.AddProduct(toothbrush, 0.99);
                var apples = new Product("apples", ProductUnit.Kilo);
                catalog.AddProduct(apples, 1.99);

                var cart = new ShoppingCart();
                if (applesQuantity != 0)
                    cart.AddItemQuantity(apples, applesQuantity); 

                if (toothbrushQuantity != 0)
                cart.AddItemQuantity(toothbrush, toothbrushQuantity);

                var teller = new Teller(catalog);
                teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, apples, argument);
                teller.AddSpecialOffer(SpecialOfferType.FiveForAmount, toothbrush, argument);

                // ACT
                var receipt = teller.ChecksOutArticlesFrom(cart);

                // ASSERT
                string result = new ReceiptPrinter().PrintReceipt(receipt);
                //Approvals.Verify(result);
                return result;
            }, productDataSet, productDataSet,argumentDataSet);
        }
    }
}