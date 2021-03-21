using System;
using System.Data;
using System.Web.Mvc;
using ShopBridge.DataObjects;
using ShopBridge.Web.Models;

namespace ShopBridge.Web.Controllers
{
    public class ProductController : Controller
    {
        /// <summary>
        /// Listing of all products
        /// </summary>
        [HandleError]
        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                ProductInput model = new ProductInput();
                model.lstProduct = TblProduct.GetAllProduct();
                return View(model.lstProduct);
            }
            catch (Exception)
            {
                throw new Exception();
            }

        }

        // GET: Product/Create
        [HandleError]
        [HttpGet]
        public ActionResult Create()
        {
            try
            {
                ProductInput model = new ProductInput();
                return View(model);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Add a new product
        /// </summary>
        [HandleError]
        [HttpPost]
        public ActionResult Create(ProductInput model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblProduct Item = new TblProduct();
                    Item.ProductName = model.ProductName;
                    Item.Description = model.Description;
                    Item.Price = model.Price;
                    Int64 productId = Item.Insert();
                    return RedirectToAction("Index");
                }
                else
                {
                    return View();
                }
                
            }

            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// fetch the product details based on id
        /// </summary>
        [HandleError]
        [HttpGet]
        public ActionResult Edit(long id)
        {
            try
            {
                ProductInput model = new ProductInput();
                DataSet dsproduct = TblProduct.SelectProductByID(id);
                if (dsproduct != null && dsproduct.Tables.Count > 0 && dsproduct.Tables[0].Rows.Count > 0)
                {
                    model.ProductID = Convert.ToInt64(dsproduct.Tables[0].Rows[0]["ProductId"]);
                    model.ProductName = Convert.ToString(dsproduct.Tables[0].Rows[0]["ProductName"]);
                    model.Description = Convert.ToString(dsproduct.Tables[0].Rows[0]["Description"]);
                    model.Price = Convert.ToDecimal(dsproduct.Tables[0].Rows[0]["Price"]);
                }
                return View(model);
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Update existing product
        /// </summary>
        [HandleError]
        [HttpPost]
        public ActionResult Edit(ProductInput model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TblProduct Item = new TblProduct();
                    Item.ProductID = model.ProductID;
                    Item.ProductName = model.ProductName;
                    Item.Description = model.Description;
                    Item.Price = model.Price;
                    Item.Update();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// delete product details
        /// </summary>
        public ActionResult Delete(int id)
        {
            try
            {
                string successMsg = TblProduct.Delete(id);
                if (successMsg == "")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return Json(new object[] { false, successMsg }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception e)
            {
                return Json(new object[] { false, e.Message });
            }
            
        }

    }
}
