using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopBridge.DataObjects;
using ShopBridge.Web.Models;
using System.Data;

namespace ShopBridge.UnitTest.Controllers
{
    [TestFixture]
    public class ProductTest
    {
        /// <summary>
        ///  fetching all products
        /// </summary>
        [Test]
        public void Index()
        {
            ProductInput model = new ProductInput();
            model.lstProduct = TblProduct.GetAllProduct();
            //Checks for count of records
            Assert.AreNotEqual(model.lstProduct.Count, 0);
        }

        /// <summary>
        /// Save new product details
        /// </summary>
        [Test]
        public void Create(ProductInput model)
        {
            //Check for column values types
            Assert.AreEqual(model.ProductName, typeof(string));
            Assert.AreEqual(model.Description, typeof(string));
            Assert.AreEqual(model.Price, typeof(decimal));

        }

        /// <summary>
        ///  fetch the product details based on id
        /// </summary>
        [Test]
        public void Edit(long id)
        {
            ProductInput model = new ProductInput();
            DataSet dsproduct = TblProduct.SelectProductByID(id);
            //Checks for count of records
            Assert.AreNotEqual(dsproduct.Tables.Count, 0);
            Assert.AreNotEqual(dsproduct.Tables[0].Rows.Count, 0);
            //Check for columns
            Assert.AreNotEqual(dsproduct.Tables[0].Columns.Count, 0);
            Assert.AreEqual(dsproduct.Tables[0].Columns.Count, 13);
            //Check for column values types
            Assert.AreEqual(dsproduct.Tables[0].Columns["ProductId"].DataType, typeof(Int64));
            Assert.AreEqual(dsproduct.Tables[0].Columns["ProductName"].DataType, typeof(string));
            Assert.AreEqual(dsproduct.Tables[0].Columns["Description"].DataType, typeof(string));
            Assert.AreEqual(dsproduct.Tables[0].Columns["Price"].DataType, typeof(decimal));
            //Check for column value correctness
            Assert.AreNotEqual(dsproduct.Tables[0].Rows[0]["ProductId"], DBNull.Value);
            Assert.AreNotEqual(dsproduct.Tables[0].Rows[0]["ProductName"], DBNull.Value);
            Assert.AreNotEqual(dsproduct.Tables[0].Rows[0]["Description"], DBNull.Value);
            Assert.AreNotEqual(dsproduct.Tables[0].Rows[0]["Price"], DBNull.Value);
        }

        /// <summary>
        ///  Update existing product
        /// </summary>
        [Test]
        public void Edit(ProductInput model)
        {
            //Check for column values types
            Assert.AreEqual(model.ProductID, typeof(Int64));
            Assert.AreEqual(model.ProductName, typeof(string));
            Assert.AreEqual(model.Description, typeof(string));
            Assert.AreEqual(model.Price, typeof(decimal));
        }

        /// <summary>
        ///  delete existing product
        /// </summary>
        [Test]
        public void Delete(int id)
        {
            string successMsg = TblProduct.Delete(id);
            Assert.AreEqual(successMsg, "");
        }
    }
}
          
