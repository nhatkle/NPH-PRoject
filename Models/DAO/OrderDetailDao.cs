using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class OrderDetailDao
    {
        NPHDbContext db = null;
        public OrderDetailDao()
        {
            db = new NPHDbContext();
        }
        public bool Insert(OrderDetail detail)
        {
            try
            {
                db.OrderDetails.Add(detail);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
         
        }
        public List<Product> ListRelatedProduct(long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList();
        }
        public List<OrderDetail> ListDetail(long detailId)
        {
            var details = db.Orders.Find(detailId);
            return db.OrderDetails.Where(x => x.OrderID != detailId && x.OrderID == details.ID).ToList();
        }
    }
}
