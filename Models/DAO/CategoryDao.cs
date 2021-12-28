using Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class CategoryDao
    {
        NPHDbContext db = null;
        public CategoryDao()
        {
            db = new NPHDbContext();
        }
        public  List<BlogCategory> ListAll()
        {
            return db.BlogCategories.Where(x => x.Status == true).ToList();
        }

        public ProductCategory ViewDetail(long id)
        {
            return db.ProductCategories.Find(id);
        }
    }
}
