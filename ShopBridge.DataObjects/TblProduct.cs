using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace ShopBridge.DataObjects
{
    public class TblProduct
    {
        #region Fields
        /// <summary>
        /// The productID
        /// </summary>
        private long productID;

        /// <summary>
        /// The product Name
        /// </summary>
        private string productName;

        /// <summary>
        /// The product description
        /// </summary>
        private string description;

        /// <summary>
        /// The product price
        /// </summary>
        private decimal price;

        /// <summary>
        ///  Product Lists
        /// </summary>
        public List<TblProduct> lstProduct { get; set; }
        #endregion
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the TblProduct class.
        /// </summary>
        public TblProduct()
        {
            InitVariables();
        }

        /// <summary>
        /// Initializes a new instance of the TblProduct class.
        /// </summary>
        /// <param name="ds">The ds.</param>
        public TblProduct(DataSet ds)
        {
            MakeObject(ds);
        }
        #endregion
        #region Properties
        /// <summary>
        /// Gets or sets the ProductID value.
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        public virtual long ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
        /// <summary>
        /// Gets or sets the Product Name .
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        public virtual string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        /// <summary>
        /// Gets or sets the Product Description.
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        public virtual string Description
        {
            get { return description; }
            set { description = value; }
        }
        /// <summary>
        /// Gets or sets the Product Price.
        /// </summary>
        /// <value>
        /// The name of the item.
        /// </value>
        public virtual decimal Price
        {
            get { return price; }
            set { price = value; }
        }

        #endregion
        #region Methods
        /// <summary>
        /// Initialize Variables
        /// </summary>
        public void InitVariables()
        {
            this.productID = 0;
            this.productName = string.Empty;
            this.description = string.Empty;
            this.price = 0;
        }

        /// <summary>
        /// Create object by DataSet
        /// </summary>
        /// <param name="ds">The ds.</param>
        private void MakeObject(DataSet ds)
        {
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                MakeObject(ds.Tables[0].Rows[0]);
            }
            else
                InitVariables();
        }
        
        /// <summary>
        /// Create object by DataRow
        /// </summary>
        /// <param name="dr">The dr.</param>
        private void MakeObject(DataRow dr)
        {
            DataTable dt = dr.Table;
            if (dt.Columns.Contains("ProductId"))
            {
                long.TryParse(Convert.ToString(dr["ProductId"]), out productID);
            }
            if (dt.Columns.Contains("ProductName"))
            {
                productName = Convert.ToString(dr["ProductName"]);
            }
            if (dt.Columns.Contains("Description"))
            {
                description = Convert.ToString(dr["Description"]);
            }
            if (dt.Columns.Contains("Price"))
            {
                price = Convert.ToDecimal(dr["Price"]);
            }

        }
        /// <summary>
        /// Dataset return as list Format
        /// </summary>
        /// <param name="ds">The ds.</param>
        /// <returns></returns>
        public static List<TblProduct> SelectList(DataSet ds)
        {
            List<TblProduct> lstItem = new List<TblProduct>();
            TblProduct objitem = null;

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    objitem = new TblProduct();
                    objitem.MakeObject(dr);
                    lstItem.Add(objitem);
                }
            }
            return lstItem;
        }

        public static List<TblProduct> GetAllProduct()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("proc_GetAllProduct"))
            {
                return SelectList(db.ExecuteDataSet(dbCommand));
            }
        }
        /// <summary>
        /// Inserts a record into the tblProduct table.
        /// </summary>
        /// <returns></returns>
        /// 
        public Int64 Insert()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("proc_SaveProduct"))
            {
                db.AddInParameter(dbCommand, "ProductName", DbType.String, productName);
                db.AddInParameter(dbCommand, "Description", DbType.String, description);
                db.AddInParameter(dbCommand, "Price", DbType.Decimal, price);
                // Execute the query and return the new identity value
                Int64 returnValue = Convert.ToInt64(db.ExecuteScalar(dbCommand));

                return returnValue;
            }
        }
        /// <summary>
        /// Updates a record in the tblProduct table.
        /// </summary>
        public void Update()
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("proc_UpdateProduct"))
            {
                db.AddInParameter(dbCommand, "ProductId", DbType.Int64, productID);
                db.AddInParameter(dbCommand, "ProductName", DbType.String, productName);
                db.AddInParameter(dbCommand, "Description", DbType.String, description);
                db.AddInParameter(dbCommand, "Price", DbType.Decimal, price);
                db.ExecuteNonQuery(dbCommand);
            }
            db = null;
        }
        /// <summary>
        /// Selects a single record from the tblProduct table.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns>
        /// DataSet
        /// </returns>
        public static DataSet SelectProductByID(Int64 productId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("proc_GetProductbyID"))
            {
                db.AddInParameter(dbCommand, "ProductId", DbType.Int64, productId);

                return db.ExecuteDataSet(dbCommand);
            }
        }

        /// <summary>
        /// Deletes a record from the tblProduct table by a primary key.
        /// </summary>
        /// <param name="ProductId">The programme identifier.</param>
        /// <returns>
        /// String
        /// </returns>
        public static string Delete(Int64 productId)
        {
            // Create Database object
            Database db = DatabaseFactory.CreateDatabase();
            // Create a suitable command type and add the required parameter
            using (DbCommand dbCommand = db.GetStoredProcCommand("proc_DeleteProduct"))
            {
                db.AddInParameter(dbCommand, "ProductId", DbType.Int64, productId);

                try
                {
                    db.ExecuteNonQuery(dbCommand);
                }
                catch (System.Data.SqlClient.SqlException sqlEx)
                {
                    if (sqlEx.Number == 547 || sqlEx.Number == 50000)
                        return "Can Not Delete Product";
                    else
                        throw sqlEx;
                }
            }
            db = null;
            return string.Empty;
        }
        #endregion
    }
}
