using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NPH_Project.Models
{
    public class CartItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
    public class Cart
    {
        List<CartItem> items = new List<CartItem>();
        public IEnumerable<CartItem> Items
        {
            get { return items; }
        }

        public void Add(Product pro, int qua = 1)
        {
            var item = items.FirstOrDefault(s => s.Product.ID == pro.ID);
            if (item == null)
            {
                items.Add(new CartItem
                {
                    Product = pro,
                    Quantity = qua
                });
            }
            else
            {
                item.Quantity += qua;
            }
        }


        public void UpdateQuantity(long id, int qua)
        {
            var item = items.Find(s => s.Product.ID == id);
            if (item != null)
            {
                item.Quantity = qua;
            }
        }

        public decimal TotalMoney()
        {
            var total = items.Sum(s => s.Product.Price * s.Quantity);
            return (decimal)total;
        }
        public void RemoveItem(long id)
        {
            items.RemoveAll(s => s.Product.ID == id);
        }
        //Total shopping
        public int TotalQuantityCart()
        {
            return items.Sum(s => s.Quantity);
        }
        public void ClearCart()
        {
            items.Clear(); //Clear cart for new order
        }
    }
}