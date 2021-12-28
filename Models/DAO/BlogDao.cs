using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class BlogDao
    {
        NPHDbContext db = null;
        public BlogDao()
        {
            db = new NPHDbContext();
        }
        public Blog GetbyID(long id)
        {
            return db.Blogs.Find(id);
        }

        public long Insert(Blog entity)
        {
            entity.CreatedDate = DateTime.Now;
            db.Blogs.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public IEnumerable<Blog> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Blog> model = db.Blogs;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ID).ToPagedList(page, pageSize);
        }

    }
}
