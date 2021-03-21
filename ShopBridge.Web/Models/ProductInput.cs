using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ShopBridge.DataObjects;

namespace ShopBridge.Web.Models
{
    public class ProductInput
    {
        #region "Property"

        /// <summary>
        /// Product ID 
        /// </summary>
        public long ProductID { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Please enter Product name.")]
        public string ProductName { get; set; }

        /// <summary>
        /// Product Description 
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Product Price
        /// </summary>
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        public decimal Price { get; set; }

        /// <summary>
        ///  Product Lists
        /// </summary>
        public List<TblProduct> lstProduct { get; set; }
        #endregion
    }
}