using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class OrderDao
    {
        NPHDbContext db = null;
        public OrderDao()
        {
            db = new NPHDbContext();
        }
        public long Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }

   
        public IEnumerable<Order> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Order> model = db.Orders;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.ShipName.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public Order ViewDetail(long id)
        {
            return db.Orders.Find(id);
        }

        public List<OrderDetail> ListOrderDetail(long detailID)
        {
            var order = db.Orders.Find(detailID);
            return db.OrderDetails.Where(x => x.OrderID == detailID && x.OrderID == order.ID).ToList();
        }




    }
}
