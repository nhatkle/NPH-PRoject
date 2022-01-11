using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class SlideDao
    {
        NPHDbContext db = null;
        public SlideDao()
        {
            db = new NPHDbContext();
        }
        public List<Slide> ListAll()
        {
            return db.Slides.Where(x => x.Status == true).OrderBy(y => y.DisplayOrder).ToList();
        }

        public long Insert(Slide entity)
        {
            entity.CreatedDate = DateTime.Now;
            db.Slides.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool ChangeStatus(long id)
        {
            var slide = db.Slides.Find(id);
            slide.Status = !slide.Status;
            db.SaveChanges();
            return (bool)slide.Status;
        }
        public bool Updateimage(Slide entity)
        {
            try
            {
                var slide = db.Slides.Find(entity.ID);
                slide.Image = entity.Image;
                slide.ModifiedDate = DateTime.Now;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }

        public bool Update(Slide entity)
        {
            try
            {
                var slider = db.Slides.Find(entity.ID);
                slider.DisplayOrder = entity.DisplayOrder;
                slider.Link = entity.Link;
                slider.Description = entity.Description;
                slider.ModifiedDate = DateTime.Now;
                slider.ModifiedBy = entity.ModifiedBy;
                slider.Status = entity.Status;
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
                var slide = db.Slides.Find(id);
                db.Slides.Remove(slide);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IEnumerable<Slide> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Slide> model = db.Slides;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Description.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }
        public Slide ViewDetail(int id)
        {
            return db.Slides.Find(id);
        }
    }
}
