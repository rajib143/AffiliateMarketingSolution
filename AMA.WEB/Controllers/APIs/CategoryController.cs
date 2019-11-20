using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AMA.DataLayer.Data;
using AMA.WEB.Models;

namespace AMA.WEB.Controllers.API
{
   
    public class CategoryController : ApiController
    {
        private AMAManager _AMAManager = new AMAManager();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        LootLoOnlineDatabaseEntities db = new LootLoOnlineDatabaseEntities();

      
        // GET: api/Category
        public  IQueryable<CategoryModel> GetCategories()
        {
            List<Category> categories = _AMAManager.Client.Admin.GetCategories(x=>x.ParentId==0, log).Result.ToList();
            List<CategoryModel> categoryModel = AMAManager.FillRecursive(categories, 0);

            return categoryModel.ToList().AsQueryable();
        }

      
        // GET: api/Category/5
        [ResponseType(typeof(CategoryModel))]
        public async Task<IHttpActionResult> GetCategoryByParentID(int ParentId)
        {
            List<GetParentChildCategories_Result> ParentChildCategories = _AMAManager.Client.Admin.GetParentChildsCategories(ParentId, log).Result.ToList();

            List<Category> categories = ParentChildCategories.Select(x => new Category() { 
                Id=x.Id.Value,
                Description=x.Description,
                Name=x.Name,
                ParentId=x.ParentId,
                SiteName=x.SiteName
            
            }).ToList();

            List<CategoryModel> categoryModel = AMAManager.FillRecursive(categories, ParentId);

            if (categoryModel == null)
            {
                return NotFound();
            }

            return Ok(categoryModel);
        }


        // GET: api/Category/5
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> GetCategory(int id)
        {
            Category category = await db.Categories.FindAsync(id);

           
            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        // PUT: api/Category/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }

            db.Entry(category).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Category
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> PostCategory(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Categories.Add(category);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = category.Id }, category);
        }

        // DELETE: api/Category/5
        [ResponseType(typeof(Category))]
        public async Task<IHttpActionResult> DeleteCategory(int id)
        {
            Category category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            db.Categories.Remove(category);
            await db.SaveChangesAsync();

            return Ok(category);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CategoryExists(int id)
        {
            return db.Categories.Count(e => e.Id == id) > 0;
        }
    }
}