using Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DAO
{
    public class ProductDao
    {
        NPHDbContext db = null;
        public ProductDao()
        {
            db = new NPHDbContext();
        }

        public long Insert(Product entity)
        {
            entity.CreatedDate = DateTime.Now;
            db.Products.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool ChangeStatus(long id)
        {
            var pdao = db.Products.Find(id);
            pdao.Status = !pdao.Status;
            db.SaveChanges();
            return (bool)pdao.Status;
        }
        public bool Update(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Name = entity.Name;
                product.Code = entity.Code;
                product.MetaTitle = entity.MetaTitle;
                product.Description = entity.Description;
               // product.Image = entity.Image;
                product.Size = product.Size;
                product.MoreImage = entity.MoreImage;
                product.Price = entity.Price;
                product.OriginalPrice = entity.OriginalPrice;
                product.DiscountID = entity.DiscountID;
                product.IncludedVAT = entity.IncludedVAT;
                product.Quantity = entity.Quantity;
                product.CategoryID = entity.CategoryID;
                product.Detail = entity.Detail;
                product.Warranty = entity.Warranty;
                product.CreatedBy = entity.CreatedBy;
                product.ModifiedDate = DateTime.Now;
                product.ModifiedBy = entity.ModifiedBy;
                product.MetaKeywords = entity.MetaKeywords;
                product.MetaDescriptions = entity.MetaDescriptions;
                product.Status = entity.Status;
                product.TopHot = entity.TopHot;
                product.ViewCount = entity.ViewCount;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                //logging
                return false;
            }
        }

        public bool Updateimage(Product entity)
        {
            try
            {
                var product = db.Products.Find(entity.ID);
                product.Image = entity.Image;
                product.ModifiedDate = DateTime.Now;
                product.ViewCount = entity.ViewCount;
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
                var product = db.Products.Find(id);              
                db.Products.Remove(product);
                db.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public IEnumerable<Product> ListAllPaging(string searchString, int page, int pageSize)
        {
            IQueryable<Product> model = db.Products;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

       


        //CLIENT
        public List<Product> ListAll()
        {
            return db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).ToList();
        }
        public List<Product> ListByCategoryId(long categoryID,ref int totalRecord, int pageIndex =1, int pageSize = 4)
        {
            totalRecord = db.Products.Where(x => x.CategoryID == categoryID).Count();
            var model = db.Products.Where(x => x.CategoryID == categoryID).OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return model;
        }
        
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.Where(x => x.Status == true).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListFeatureProduct(int top)
        {
            return db.Products.Where(x => x.TopHot != null && x.TopHot > DateTime.Now).OrderByDescending(x => x.CreatedDate).Take(top).ToList();
        }

        public List<Product> ListRelatedProduct(long productId)
        {
            var product = db.Products.Find(productId);
            return db.Products.Where(x => x.ID != productId && x.CategoryID == product.CategoryID).ToList(); 
        }

        public Product ViewDetail(long id)
        {
            return db.Products.Find(id);
        }


        public List<string> ListName(string keyword)
        {
            return db.Products.Where(x => x.Name.Contains(keyword)).Select(x => x.Name).ToList();
        }


        public List<ProductViewModel> Search(string keyword, ref int totalRecord, int pageIndex = 1, int pageSize = 10)
        {
            totalRecord = db.Products.Where(x => x.Name == keyword).Count();
            var model = (from a in db.Products
                         join b in db.ProductCategories
                         on a.CategoryID equals b.ID
                         where a.Name.Contains(keyword)
                         select new
                         {
                             CateMetaTitle = b.MetaTitle,
                             CateName = b.Name,
                             CreatedDate = a.CreatedDate,
                             ID = a.ID,
                             Images = a.Image,
                             Name = a.Name,
                             MetaTitle = a.MetaTitle,
                             Price = a.Price
                         }).AsEnumerable().Select(x => new ProductViewModel()
                         {
                             CateMetaTitle = x.MetaTitle,
                             CateName = x.Name,
                             CreatedDate = x.CreatedDate,
                             ID = (int)x.ID,
                             Images = x.Images,
                             Name = x.Name,
                             MetaTitle = x.MetaTitle,
                             Price = x.Price
                         });
            model.OrderByDescending(x => x.CreatedDate).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            return model.ToList();
        }

        //Cart
      

    }
}
