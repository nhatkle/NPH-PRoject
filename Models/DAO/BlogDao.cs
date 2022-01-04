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
        public Blog ViewDetail(long id)
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

        public bool Update(Blog entity)
        {
            try
            {
                var blog = db.Blogs.Find(entity.ID);
                blog.Name = entity.Name;

                blog.MetaTitle = entity.MetaTitle;
                blog.Description = entity.Description;
                // blog.Image = entity.Image;
                blog.BlogCategoryID = entity.BlogCategoryID;
                blog.Detail = entity.Detail;
                blog.CreatedBy = entity.CreatedBy;
                blog.ModifiedDate = DateTime.Now;
                blog.ModifiedBy = entity.ModifiedBy;
                blog.MetaKeywords = entity.MetaKeywords;
                blog.MetaDescriptions = entity.MetaDescriptions;
                blog.Status = entity.Status;
                blog.TopHot = entity.TopHot;
                blog.ViewCount = entity.ViewCount;
                blog.Tags = entity.Tags;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }

        public bool Updateimage(Blog entity)
        {
            try
            {
                var blog = db.Blogs.Find(entity.ID);
                blog.Image = entity.Image;
                blog.ModifiedDate = DateTime.Now;
                blog.ViewCount = entity.ViewCount;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var blog = db.Blogs.Find(id);
                db.Blogs.Remove(blog);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public List<Blog> ListAll()
        {
            return db.Blogs.OrderByDescending(x => x.CreatedDate).ToList();
        }
    }
}
