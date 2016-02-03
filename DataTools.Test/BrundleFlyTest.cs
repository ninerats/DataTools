using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Craftsmaneer.DataTools.Compare;
using Craftsmaneer.DataTools.Test.IO;
using NUnit.Framework;

namespace Craftsmaneer.DataTools.Test
{
    [TestFixture]
    public class BrundleFlyTest
    {
        [Test]
        public void RoundTripComparisonTest()
        {
            var sw = new Stopwatch();
            Debug.WriteLine("Starting Test:");
            sw.Start();
            var importFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FolderDTC");
            var importDts = new FolderDataTableSet(importFolder)
            {
                Id = "Import DataSet",
                TableList = TableList
            };

            var connStr = DataSerTestHelper.DataDiffConnectionString;

            Debug.WriteLine(string.Format("Starting import...  elpased: {0}", sw.Elapsed));
            var importResult = importDts.ImportTables(connStr);
            Debug.WriteLine(string.Format("Import complete: {0}", sw.Elapsed));
            DataSerTestHelper.AssertResult(importResult);
            Debug.WriteLine(string.Format("Starting export...  elpased: {0}", sw.Elapsed));
            var exportFolder = TestHelper.ResetFolder("Export");
            var exportDts = new DatabaseDataTableSet(connStr)
            {
                Id = "Export DataSet",
                TableList = TableList
            };

            var exportResult = exportDts.ExportTables(exportFolder);
            Debug.WriteLine(string.Format("export complete: {0}", sw.Elapsed));
            DataSerTestHelper.AssertResult(exportResult);

            Debug.WriteLine("Asserting exported files are the same as the originals...");
            AssertFolderContentsAreEqual(importFolder, exportFolder);
            Debug.WriteLine(string.Format("Total time: {0}", sw.Elapsed));

        }

        private List<string> TableList
        {
            get
            {
                return new[]
                {
                    "dbo.AWBuildVersion",
                    "dbo.DatabaseLog",
                    "dbo.ErrorLog",
                    "HumanResources.Department",
                    "HumanResources.EmployeeDepartmentHistory",
                    "HumanResources.EmployeePayHistory",
                    "HumanResources.JobCandidate",
                    "HumanResources.Shift",
                    "Person.AddressType",
                    "Person.BusinessEntity",
                    "Person.BusinessEntityAddress",
                    "Person.BusinessEntityContact",
                    "Person.ContactType",
                    "Person.CountryRegion",
                    "Person.EmailAddress",
                    "Person.Password",
                    "Person.Person",
                    "Person.PersonPhone",
                    "Person.PhoneNumberType",
                    "Person.StateProvince",
                    "Production.BillOfMaterials",
                    "Production.Illustration",
                    "Production.Location",
                    "Production.Product",
                    "Production.ProductCategory",
                    "Production.ProductCostHistory",
                    "Production.ProductDescription",
                    "Production.ProductInventory",
                    "Production.ProductListPriceHistory",
                    "Production.ProductModel",
                    "Production.ProductModelIllustration",
                    "Production.ProductModelProductDescriptionCulture",
                    "Production.ProductPhoto",
                    "Production.ProductProductPhoto",
                    "Production.ProductReview",
                    "Production.ProductSubcategory",
                    "Production.ScrapReason",
                    "Production.TransactionHistory",
                    "Production.TransactionHistoryArchive",
                    "Production.UnitMeasure",
                    "Production.WorkOrder",
                    "Production.WorkOrderRouting",
                    "Purchasing.ProductVendor",
                    "Purchasing.PurchaseOrderDetail",
                    "Purchasing.PurchaseOrderHeader",
                    "Purchasing.ShipMethod",
                    "Purchasing.Vendor",
                    "Sales.CountryRegionCurrency",
                    "Sales.CreditCard",
                    "Sales.Currency",
                    "Sales.CurrencyRate",
                    "Sales.Customer",
                    "Sales.PersonCreditCard",
                    "Sales.SalesOrderDetail",
                    "Sales.SalesOrderHeader",
                    "Sales.SalesOrderHeaderSalesReason",
                    "Sales.SalesPerson",
                    "Sales.SalesPersonQuotaHistory",
                    "Sales.SalesReason",
                    "Sales.SalesTaxRate",
                    "Sales.SalesTerritory",
                    "Sales.SalesTerritoryHistory",
                    "Sales.ShoppingCartItem",
                    "Sales.SpecialOffer",
                    "Sales.SpecialOfferProduct",
                    "Sales.Store"
                }.ToList();
            }
        }

        public void AssertFolderContentsAreEqual(string folder1, string folder2)
        {
            if (folder1 == folder2)
            {
                Debug.WriteLine("folder1 and folder2 are the same folder.");
                return;
            }

            var folder1Files = new DirectoryInfo(folder1).GetFiles();
            var folder2Files = new DirectoryInfo(folder2).GetFiles();

            var filteredList = TableList.Except(new[] {"dbo.DatabaseLog"});


            foreach (var table in filteredList)
            {
                var fileName = string.Format("{0}.xml", table);
                var folder1File = folder1Files.FirstOrDefault(f1f => f1f.Name == fileName);
                Assert.IsNotNull(folder1File, string.Format("can't find {0} in folder {1}", fileName, folder1));
                var folder2File = folder2Files.FirstOrDefault(f2f => f2f.Name == fileName);
                Assert.IsNotNull(folder2File, string.Format("can't find {0} in folder {1}", fileName, folder2));
                Debug.WriteLine(string.Format("Comparing the contents of {0} to {1}...",
                    folder1File.FullName, folder2File.FullName));
                var folder1FileContents = File.ReadAllText(folder1File.FullName);
                var folder2FileContents = File.ReadAllText(folder2File.FullName);
                Assert.AreEqual(NormalizeText(folder1FileContents), NormalizeText(folder2FileContents));
            }
        }

        private string NormalizeText(string text)
        {
            var working = text.Replace("\r", "");
            return working;
        }
        [Test]
        public void CompareInputToOutput()
        {
            var importFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FolderDTC");
            var outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Export");
            AssertFolderContentsAreEqual(importFolder, outputFolder);
        }
    }



}
