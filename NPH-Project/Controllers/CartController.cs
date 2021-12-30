using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models.DAO;
using Models.EF;
using NPH_Project.Models;
using System.Web.Script.Serialization;

namespace NPH_Project.Controllers
{
    public class CartController : Controller
    {
        private const string CartSession = "CartSession";
        // GET: Cart
        public ActionResult Index()
        {
            var cart = Session[CartSession];
            var list = new List<CartItem>();
            if (cart != null)
            {
                list = (List<CartItem>)cart;
            }
            return View(list);
        }
        public ActionResult AddItem(long productId, int quantity)
        {
            var product = new ProductDao().ViewDetail(productId);
            var cart = Session[CartSession];
            if(cart != null)
            {
                var list = (List<CartItem>)cart;
                if(list.Exists(x=>x.Product.ID == productId))
                {
                    foreach (var item in list)
                    {
                        if (item.Product.ID == productId)
                        {
                            item.Quantity += quantity;
                        }
                    }
                }
                else
                {
                    // New Item
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                    
                }
                //Get to session
                Session[CartSession] = list;

            }
            else
            {
                // New Item
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);
                //Get to session
                Session[CartSession] = list;
            } 
            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel)
        {
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);
            var sessionCart =(List<CartItem>)Session[CartSession];
            foreach(var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }   
            }
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(long productId)
        {
            var sessionCart = (List<CartItem>)Session[CartSession];
            sessionCart.RemoveAll(x => x.Product.ID == productId);
            Session[CartSession] = sessionCart;
            return Json(new
            {
                status = true
            });
        }
        /*NPHDbContext db = new NPHDbContext();
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        public ActionResult AddItem(long id)
        {
            var pro = db.Products.SingleOrDefault(s => s.ID == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult UpdateQuantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            long id_pro = long.Parse(form["Product_ID"]);
            int quantity = int.Parse(form["Quantity"]);
            cart.UpdateQuantity(id_pro, quantity);
            return RedirectToAction("Index", "Cart");
        }
        public ActionResult Index()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("Index", "Cart");
            Cart cart = Session["Cart"] as Cart;
            return View(cart);
        }
        public ActionResult RemoveItem(long id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.RemoveItem(id);
            return RedirectToAction("Index", "Cart");
        }

        public PartialViewResult BagCart()
        {
            int total_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)

                total_item = cart.TotalQuantityCart();
            ViewBag.QuantityCart = total_item;
            return PartialView("BagCart");
        }
        public ActionResult ShoppingSuccess()
        {
            return View();
        }
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Order order = new Order();
                order.CreatedDate = DateTime.Now;
                order.CustomerID = long.Parse(form["CodeCustomer"]);
                order.ShipAddress = form["AddressDelivery"];
                db.Orders.Add(order);
                foreach (var item in cart.Items)
                {
                    OrderDetail order_Detail = new OrderDetail();
                    order_Detail.OrderID = order.ID;
                    order_Detail.ProductID = item.Product.ID;
                    order_Detail.Price = item.Product.Price;
                    order_Detail.Quantity = item.Quantity;
                    db.OrderDetails.Add(order_Detail);
                }
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("ShoppingSuccess", "Cart");
            }
            catch
            {
                return Content("Error Checkout.Please information of Customer...");

            }
        }*/


    }
}