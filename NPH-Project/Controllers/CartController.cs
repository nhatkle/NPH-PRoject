﻿using System;
using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using Common;
using Models.EF;
using NPH_Project.Models;

namespace NPH_Project.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        NPHDbContext db = new NPHDbContext();
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
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Assets/Client/template/neworder.html"));
           
            try
            {
                Cart cart = Session["Cart"] as Cart;
                Order order = new Order();
                order.CreatedDate = DateTime.Now;
                order.ShipName = form["ShipName"];
                order.ShipMobile = form["ShipMobile"];
                order.ShipAddress = form["ShipAddress"];
                order.ShipEmail = form["ShipEmail"];
                order.Total = cart.TotalMoney();
                db.Orders.Add(order);
                foreach (var item in cart.Items)
                {
                    OrderDetail order_Detail = new OrderDetail();
                    order_Detail.OrderID = order.ID;
                    order_Detail.ProductID = item.Product.ID;
                    order_Detail.Price = item.Product.Price;
                    order_Detail.Quantity = item.Quantity;
                   
                    content = content.Replace("{{ProductName}}", item.Product.Name);
                    content = content.Replace("{{Price}}", item.Product.Price.ToString());
                    content = content.Replace("{{Quantity}}", item.Quantity.ToString());
                    db.OrderDetails.Add(order_Detail);
                }
               
                
                content = content.Replace("{{CustomerName}}", form["ShipName"]);
                content = content.Replace("{{Phone}}", form["ShipMobile"]);
                content = content.Replace("{{Email}}", form["ShipEmail"]);
                content = content.Replace("{{Address}}", form["ShipAddress"]);
                content = content.Replace("{{Total}}", order.Total.ToString());
            
                var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

                new MailHelper().SendMail(form["ShipEmail"], "New Order from NPH Project Shop", content);
                new MailHelper().SendMail(toEmail, "New Order from NPH Project Shop", content);
                db.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("ShoppingSuccess", "Cart");
            }
            catch
            {
                return Content("Error Checkout.Please information of Customer...");

            }
        }
    }
}